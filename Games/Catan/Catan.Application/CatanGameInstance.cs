using BGS.GameAbstractions.Interfaces;
using Catan.Shared.Interfaces;

namespace Catan.Application;

public class CatanGameInstance : IGameInstance
{
    private readonly GameApplication _gameApplication;
    private readonly object _lock = new();

    public CatanGameInstance(GameApplication gameApplication)
    {
        _gameApplication = gameApplication;
    }

    public object Execute(object command)
    {
        lock (_lock)
        {
            return _gameApplication.Execute((ICommand)command);
        }
    }
}