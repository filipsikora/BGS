using Catan.Core.Snapshots;

namespace Catan.Core.Queries.Interfaces
{
    public interface IGameStateQueryService
    {
        public FullPlayerDataSnapshot GetFullPlayerData(int playerId);
    }
}
