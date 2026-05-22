using Catan.Core.Snapshots.ClientQueries;

namespace Catan.Core.Queries.Interfaces
{
    public interface IGameStateQueryService
    {
        public FullPlayerDataSnapshot GetFullPlayerData(int playerId);
    }
}
