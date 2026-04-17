namespace BGS.GameAbstractions.Interfaces
{
    public interface IGameManager
    {
        Guid CreateGame(IGameFactory factory);
        IGameInstance GetGame(Guid gameId);
        bool TryGetGame(Guid gameId, out IGameInstance game);
        IEnumerable<Guid> GetAllGamesIds();
    }
}