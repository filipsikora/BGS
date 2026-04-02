using BGS.GameAbstractions.Interfaces;
using System.Collections.Concurrent;

namespace BGS.Backend
{
    public class GameManager : IGameManager
    {
        private readonly ConcurrentDictionary<Guid, IGameInstance> _games = new();

        public GameManager() { }

        public Guid CreateGame(IGameFactory factory)
        {
            var gameId = Guid.NewGuid();
            var game = factory.CreateGame();

            _games[gameId] = game;

            return gameId;
        }

        public IGameInstance GetGame(Guid gameId)
        {
            if (!_games.TryGetValue(gameId, out var game))
                throw new Exception($"Game {gameId} not found");

            return game;
        }

        public bool TryGetGame(Guid gameId, out IGameInstance game)
        {
            return _games.TryGetValue(gameId, out game);
        }

        public IEnumerable<Guid> GetAllGamesIds()
        {
            return _games.Keys;
        }
    }
}