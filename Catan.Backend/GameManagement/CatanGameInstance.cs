using BGS.GameAbstractions.Interfaces;
using Catan.Application;
using Catan.Backend.Mappers;
using Catan.Shared.Dtos;

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

        public object Query(string queryName, object? parameters = null)
        {
            lock (_lock)
            {
                return queryName switch
                {
                    "board" => HandleBoardQuery(),
                    "player-data" => HandlePlayerDataQuery(parameters),
                    "player-cards" => HandlerPlayerCardsQuery(parameters),
                    _ => throw new Exception($"Unknown query: {queryName}")
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
            var dto = PlayerMappers.MapPlayerDataToDto(snapshot);

            return dto;
        }

        private PlayerCardsDto HandlerPlayerCardsQuery(object? param)
        {
            if (param is not int playerId)
                throw new Exception("PlayerId is required");

            var snapshot = _gameApplication.Facade.GetPlayersCards(playerId);
            var dto = PlayerMappers.MapPlayerCardsToDto(snapshot);

            return dto;
        }
    }
}