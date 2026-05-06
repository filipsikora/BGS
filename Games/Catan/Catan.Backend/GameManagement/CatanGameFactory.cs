using BGS.GameAbstractions.Interfaces;
using Catan.Application;
using Catan.Application.Controllers;
using Catan.Core;
using Catan.Core.Engine;
using Catan.Core.Queries.InMemory;
using Catan.Core.Helpers;

namespace Catan.Backend.GameManagement
{
    public class CatanGameFactory : IGameFactory
    {
        public int MaxPlayers { get; private set; } = 4;
        public int MinPlayers { get; private set; } = 2;

        public IGameInstance CreateGame(int playerNumber)
        {

            var random = new RandomProvider();
            var map = new HexMap(random);
            var gameState = new GameState(random, map);

            var firstPlayerId = gameState.InitializeNewGame(playerNumber, 1f);

            var session = new GameSession(gameState);

            var boardQuery = new InMemoryBoardQueryServices(session);
            var devCardQuery = new InMemoryDevCardQueryService(session);
            var playersQuery = new InMemoryPlayersQueryServices(session);
            var resourcesQuery = new InMemoryResourcesQueryService(session);
            var tradeQuery = new InMemoryTradeQueryServices(session);
            var turnsQuery = new InMemoryTurnsQueryService(session);
            var gameStateQuery = new InMemoryGameStateQueryServices(session, devCardQuery);

            var facade = new Facade(session, boardQuery, devCardQuery, playersQuery, resourcesQuery, tradeQuery, turnsQuery, gameStateQuery);

            var app = new GameApplication(facade);
            var registry = new CatanCommandRegistry();

            return (new CatanGameInstance(app, registry));
        }
    }
}