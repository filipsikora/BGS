using BGS.GameAbstractions.Interfaces;

namespace BGS.Persistence
{
    public class EfGameRepository : IGameRepository
    {
        public Task SaveGameAsync(Guid gameId, string stateJson, string gameType, string status)
        {

        }

        public Task<string?> LoadStateAsync(Guid gameId)
        {

        }
    }
}
