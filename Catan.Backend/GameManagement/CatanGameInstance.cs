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
    }
}