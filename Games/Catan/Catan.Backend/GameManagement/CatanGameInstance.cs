using BGS.GameAbstractions.Interfaces;
using Catan.Application;
using Catan.Backend.Mappers;
using Catan.Shared.Data;
using Catan.Shared.Dtos;
using BGS.Shared.Dtos;
using Microsoft.AspNetCore.Http;
using BGS.Shared.Data;
using Catan.Backend.Helpers;
using BGS.GameAbstractions.Models;
using Newtonsoft.Json.Linq;

namespace Catan.Backend.GameManagement
{
    public class CatanGameInstance : IGameInstance
    {
        private readonly GameApplication _gameApplication;
        private readonly CatanCommandRegistry _registry;

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
        }

        public GameApplication Application => _gameApplication; // just for testing


        public CommandResponseDto Execute(CommandRequestDto request)
        {
            lock (_lock)
            {
                if (request is not CommandRequestDto dto)
                    throw new Exception("Invalid request type");

                var command = _registry.Create(dto);
                var result = _gameApplication.Execute(command);

                return GameResultMappers.MapGameResultToDto(result);
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
                    EnumQueryName.Board => HandleBoardQuery(),
                    EnumQueryName.PlayerData => HandlePlayerDataQuery(ParseInt(dict, "playerId")),
                    EnumQueryName.ResourcesAvailability => HandleResourcesAvailabilityQuery(),
                    EnumQueryName.VictimCards => HandleVictimCardsQuery(),
                    EnumQueryName.CurrentPlayerDevCards => HandleCurrentPlayerDevCardsQuery(),
                    EnumQueryName.NotCurrentPlayerNames => HandlerNotCurrentPlayerNamesQuery(),
                    EnumQueryName.TradeOfferData => HandleTradeOfferDataQuery(),
                    EnumQueryName.SomePlayersNames => HandleSomePlayersNamesQuery(ParseListInt(dict, "playerIds")),
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

        private BoardDto HandleBoardQuery()
        {
            var snapshot = _gameApplication.Facade.GetBoardData();
            var dto = BoardMappers.MapBoardToDto(snapshot);

            return dto;
        }

        private PlayerDataDto HandlePlayerDataQuery(object? param)
        {
            if (param is not int playerId)
                throw new Exception("PlayerId is required");

            var snapshot = _gameApplication.Facade.GetPlayersData(playerId);
            var dto = PlayerMappers.MapPlayerDataToDto(snapshot);

            return dto;
        }

        private ResourcesAvailabilityDto HandleResourcesAvailabilityQuery()
        {
            var snapshot = _gameApplication.Facade.GetResourcesAvailability();
            var dto = QueryMappers.MapResourcesAvailabilityToDto(snapshot);

            return dto;
        }

        private PlayerCardsDto HandleVictimCardsQuery()
        {
            var snapshot = _gameApplication.Facade.GetVictimsCards();
            var dto = PlayerMappers.MapPlayerCardsToDto(snapshot);

            return dto;
        }

        private IReadOnlyList<DevelopmentCardDto> HandleCurrentPlayerDevCardsQuery()
        {
            var snapshot = _gameApplication.Facade.GetCurrentPlayerDevCards();
            var dto = QueryMappers.MapCurrentPlayerDevCardsToDto(snapshot);

            return dto;
        }

        private IReadOnlyList<PlayerNameDto> HandlerNotCurrentPlayerNamesQuery()
        {
            var snapshot = _gameApplication.Facade.GetNotCurrentPlayersNames();
            var dto = QueryMappers.MapNotCurrentPlayerNamesToDto(snapshot);

            return dto;
        }

        private TradeOfferedDto HandleTradeOfferDataQuery()
        {
            var snapshot = _gameApplication.Facade.GetTradeOfferData();
            var dto = QueryMappers.MapTradeOfferToDto(snapshot);

            return dto;
        }

        private IReadOnlyList<PlayerNameDto> HandleSomePlayersNamesQuery(List<int> potentialVictimsIds)
        {
            var snapshot = _gameApplication.Facade.GetSomePlayersNames(potentialVictimsIds);
            var dto = QueryMappers.MapSomePlayersNamesToDto(snapshot);

            return dto;
        }

        private int ParseInt(IQueryCollection dict, string key)
        {
            if (!dict.TryGetValue(key, out var value))
                throw new Exception($"Missing parameter: {key}");

            if (!int.TryParse(value.ToString(), out var result))
                throw new Exception($"Invalid int for {key}");

            return result;
        }

        private List<int> ParseListInt(IQueryCollection dict, string key)
        {
            return dict[key]
                .Select(v => int.Parse(v))
                .ToList();
        }
    }
}