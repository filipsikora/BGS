using Catan.Application;
using Catan.Application.Controllers;
using Catan.Backend.GameManagement;
using Catan.Shared.Data;

public class TestGame
{
    public Facade Facade { get; }
    private GameApplication _gameApplication { get; }

    private TestGame(Facade facade, GameApplication gameApplication)
    {
        Facade = facade;
        _gameApplication = gameApplication;
    }

    public static TestGame New()
    {
        var factory = new CatanGameFactory();
        var (instance, _) = factory.CreateGame();

        var catan = (CatanGameInstance)instance;
        var application = catan.Application;
        var facade = catan.Application.Facade;

        return new TestGame(facade, application);
    }

    public TestGame InNormalRound()
    {
        Facade.SetCorePhase(EnumGamePhases.NormalRound);
        return this;
    }

    public TestGame InInitialiRound()
    {
        Facade.SetCorePhase(EnumGamePhases.FirstRoundsBuilding);
        return this;
    }
}