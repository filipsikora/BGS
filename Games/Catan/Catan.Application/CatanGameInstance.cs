using BGS.GameAbstractions.Interfaces;
using Catan.Shared.Interfaces;

namespace Catan.Application;

public class CatanGameInstance : IGameInstance
{
    private readonly GameApplication _gameApplication;

    public CatanGameInstance(GameApplication gameApplication)
    {
        _gameApplication = gameApplication;
    }

    public object Execute(object command)
    {
        return _gameApplication.Execute((ICommand)command);
    }
}