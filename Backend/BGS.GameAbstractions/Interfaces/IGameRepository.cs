namespace BGS.GameAbstractions.Interfaces
{
    public interface IGameRepository
    {
        Task SaveGameAsync(Guid gameId, string stateJson, string gameType, string status);

        Task<string?> LoadStateAsync(Guid gameId);
    }
}
