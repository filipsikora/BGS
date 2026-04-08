using BGS.GameAbstractions.Interfaces;
using Catan.Application;
using Catan.Backend.Mappers;
using Catan.Shared.Data;
using Catan.Shared.Dtos;
using Microsoft.Extensions.Primitives;

namespace Catan.Backend.GameManagement
{
    public class CatanGameInstance : IGameInstance
    {
        private readonly GameApplication _gameApplication;
        private readonly CatanCommandRegistry _registry;

        private readonly object _lock = new();

        public CatanGameInstance(GameApplication gameApplication, CatanCommandRegistry registry)
        {
            _gameApplication = gameApplication;
            _registry = registry;
        }

        public object Execute(object request)
        {
            Console.WriteLine($"GameInstance hash: {GetHashCode()}");

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
                var dict = data as Dictionary<string, StringValues>;
                var query = QueryMappers.MapStringToEnum(queryName);

                return query switch
                {
                    EnumQueryName.Board => HandleBoardQuery(),
                    EnumQueryName.PlayerData => HandlePlayerDataQuery(ParseInt(dict, "playerId")),
                    EnumQueryName.PlayerCards => HandlePlayerCardsQuery(ParseInt(dict, "playerId")),
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
            var dto = QueryMappers.MapPlayerDataToDto(snapshot);

            return dto;
        }

        private PlayerCardsDto HandlePlayerCardsQuery(object? param)
        {
            if (param is not int playerId)
                throw new Exception("PlayerId is required");

            var snapshot = _gameApplication.Facade.GetPlayersCards(playerId);
            var dto = QueryMappers.MapPlayerCardsToDto(snapshot);

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
            var dto = QueryMappers.MapPlayerCardsToDto(snapshot);

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

        private int ParseInt(Dictionary<string, StringValues>? dict, string key)
        {
            if (dict == null || !dict.TryGetValue(key, out var value))
                throw new Exception($"Missing parameter: {key}");

            if (!int.TryParse(value, out var result))
                throw new Exception($"Invalid int for {key}");

            return result;
        }

        private List<int> ParseListInt(Dictionary<string, StringValues>? dict, string key)
        {
            if (dict == null || !dict.TryGetValue(key, out var values))
                throw new Exception($"Missing parameter: {key}");

            var result = new List<int>();

            foreach (var v in values)
            {
                if (!int.TryParse(v, out var parsed))
                    throw new Exception($"Invalid int in {key}");

                result.Add(parsed);
            }

            return result;
        }
    }
}   