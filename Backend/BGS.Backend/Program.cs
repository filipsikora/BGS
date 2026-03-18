using BGS.GameAbstractions.Interfaces;
using Catan.Application;
using Catan.Shared.Helpers;
using Catan.Core.Engine;
using Catan.Core;
using Catan.Core.Queries.InMemory;
using QuikGraph;
using Catan.Application.Controllers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddSingleton<IGameInstance>(sp =>
{
    var random = new RandomProvider();
    var map = new HexMap(random);
    var gameState = new GameState(random, map);

    gameState.InitializeNewGame(2, 1f);

    var session = new GameSession(gameState);

    var boardQuery = new InMemoryBoardQueryServices(session);
    var devCardQuery = new InMemoryDevCardQueryService(session);
    var playersQuery = new InMemoryPlayersQueryServices(session);
    var resourcesQuery = new InMemoryResourcesQueryService(session);
    var tradeQuery = new InMemoryTradeQueryServices(session);
    var turnsQuery = new InMemoryTurnsQueryService(session);

    var facade = new Facade(session, boardQuery, devCardQuery, playersQuery, resourcesQuery, tradeQuery, turnsQuery);

    var app = new GameApplication(facade);

    return new CatanGameInstance(app);
});

var app = builder.Build();

app.MapControllers();

app.Run();