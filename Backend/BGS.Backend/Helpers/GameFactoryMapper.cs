using BGS.Backend.Interfaces;
using BGS.GameAbstractions.Interfaces;
using Catan.Backend.GameManagement;
using BGS.Shared.Data;

namespace BGS.Backend.Helpers
{
    public class GameFactoryMapper : IGameFactoryMapper
    {
        private readonly Dictionary<EnumGames, IGameFactory> _factories;

        public GameFactoryMapper()
        {
            _factories = new()
            {
                { EnumGames.Catan, new CatanGameFactory() }
            };
        }

        public IGameFactory GetFactory(EnumGames game)
        {
            if (!_factories.TryGetValue(game, out var factory))
                throw new Exception("Game not recognized");

            return factory;
        }
    }
}