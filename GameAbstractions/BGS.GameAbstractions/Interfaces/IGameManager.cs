using BGS.GameAbstractions.Interfaces;

namespace BGS.Backend.GameManagement
{
    public interface IGameManager
    {
        Guid CreateGame();
        IGameInstance GetGame(Guid gameId);
        bool TryGetGame(Guid gameId, out IGameInstance game);
        IEnumerable<Guid> GetAllGamesIds();
    }
}
