namespace BGS.GameAbstractions.Interfaces
{
    public interface IGameManager
    {
        Guid CreateGame();
        IGameInstance GetGame(Guid gameId);
        bool TryGetGame(Guid gameId, out IGameInstance game);
        IEnumerable<Guid> GetAllGamesIds();
    }
}
