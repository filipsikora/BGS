using BGS.GameAbstractions.Interfaces;
using BGS.GameAbstractions.Models;
using BGS.Shared.Data;
using BGS.Shared.Dtos;
using Catan.Application;
using Catan.Backend.Helpers;
using Catan.Backend.Mappers;
using Catan.Shared.Data;
using Catan.Shared.Dtos;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;

namespace Catan.Backend.GameManagement
{
    public class CatanGameInstance : IGameInstance
    {
        private readonly GameApplication _gameApplication;
        private readonly CatanCommandRegistry _registry;

        private readonly DomainEventsDispatcher _eventDispatcher;

        private readonly object _lock = new();

        public Guid GameId { get; private set; }
        public EnumGameInstanceState State { get; private set; }
        public int CurrentPlayers => PlayerTokens.Count;
        public int DesiredPlayerNumber { get; private set; }

        private readonly Queue<int> _availableIds;
        public EnumGames GameType { get; }

        public Dictionary<Guid, int> PlayerTokens { get; } = new Dictionary<Guid, int>();

        public CatanGameInstance(GameApplication gameApplication, CatanCommandRegistry registry, int playerNumber)
        {
            _gameApplication = gameApplication;
            _registry = registry;
            DesiredPlayerNumber = playerNumber;
            GameType = EnumGames.Catan;
            State = EnumGameInstanceState.Created;

            _availableIds = new Queue<int>(_gameApplication.GetIdsList());
            _eventDispatcher = new DomainEventsDispatcher();
        }

        public GameApplication Application => _gameApplication; // just for testing


        public CommandExecutionResultDto Execute(CommandRequestDto request, int playerId)
        {
            lock (_lock)
            {
                if (request is not CommandRequestDto dto)
                    throw new Exception("Invalid request type");

                var command = _registry.Create(dto);
                var gameResult = _gameApplication.Execute(command, playerId);
                var commandResponseDto = GameResultMappers.MapGameResultToDto(gameResult);
                var updatesList = new List<GameUpdateDto>();

                foreach (var domainEvent in gameResult.DomainEvents)
                {
                    updatesList.AddRange(_eventDispatcher.Dispatch(domainEvent, this));
                }

                return new CommandExecutionResultDto(updatesList, commandResponseDto);
            }
        }

        public object Query(string queryName, object? data)
        {
            lock (_lock) 
            {
                var dict = data as IQueryCollection;
                var query = QueryMappers.MapStringToEnum(queryName);

                return query switch
                {
                    EnumQueryName.VictimCards => HandleVictimCardsQuery(),
                    EnumQueryName.TradeOfferData => HandleTradeOfferDataQuery(),
                    _ => throw new Exception($"Unknown query: {query}")
                };
            }
        }

        public JoinResult JoinGame(Guid? playerToken, string playerName)
        {
            lock (_lock)
            {
                if (playerToken == null)
                {
                    if (PlayerTokens.Count == DesiredPlayerNumber)
                        return new JoinResult(EnumJoinStatus.GameFull, "Game is full", null, null);

                    if (State != EnumGameInstanceState.Created && State != EnumGameInstanceState.PlayersJoining)
                        return new JoinResult(EnumJoinStatus.GameStarted, "Game already started", null, null);

                    else
                        return JoinNewPlayer(GameId, playerName);
                }

                else
                {
                    if (!PlayerTokens.ContainsKey(playerToken.Value))
                        return new JoinResult(EnumJoinStatus.TokenNotRecognized, "You are not a part of this match", null, null);

                    else
                        return RejoinGame(GameId, playerToken.Value);
                }
            }
        }

        public string GetGameStateDataString()
        {
            var snapshot = _gameApplication.GetGameStateData();
            var json = GameStateSerializer.SerializeGameState(snapshot);

            return json;
        }

        private JoinResult JoinNewPlayer(Guid gameId, string playerName)
        {
            State = EnumGameInstanceState.PlayersJoining;
            var playerId = _availableIds.Dequeue();
            var playerToken = Guid.NewGuid();
            _gameApplication.SetPlayerName(playerName, playerId);
            var initialStateSnapshot = _gameApplication.GetGameStatePerPlayerSnapshot(playerId);
            var initialStateDto = GameStatePerPlayerMappers.MapGameStatePerPlayerToDto(initialStateSnapshot, gameId, playerToken);
            var initialStateJson = JToken.FromObject(initialStateDto);

            PlayerTokens.Add(playerToken, playerId);

            return new JoinResult(EnumJoinStatus.Success, null, playerToken, initialStateJson);
        }

        private JoinResult RejoinGame(Guid gameId, Guid playerToken)
        {
            var playerId = PlayerTokens[playerToken];
            var initialStateSnapshot = _gameApplication.GetGameStatePerPlayerSnapshot(playerId);
            var initialStateDto = GameStatePerPlayerMappers.MapGameStatePerPlayerToDto(initialStateSnapshot, gameId, playerToken);
            var initialStateJson = JToken.FromObject(initialStateDto);

            return new JoinResult(EnumJoinStatus.Success, null, playerToken, initialStateJson);
        }

        private PlayerResourcesDto HandleVictimCardsQuery() // exists in Card Stealing
        {
            var snapshot = _gameApplication.Facade.GetVictimsCards();
            var dto = PlayerMappers.MapPlayerCardsToDto(snapshot);

            return dto;
        }

        private TradeOfferedDto HandleTradeOfferDataQuery() // exists in Trade Request
        {
            var snapshot = _gameApplication.Facade.GetTradeOfferData();
            var dto = QueryMappers.MapTradeOfferToDto(snapshot);

            return dto;
        }
    }
}