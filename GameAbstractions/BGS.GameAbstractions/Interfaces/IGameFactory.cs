using BGS.GameAbstractions.Interfaces;

namespace BGS.Backend.GameManagement
{
    public interface IGameFactory
    {
        IGameInstance CreateGame();
    }
}