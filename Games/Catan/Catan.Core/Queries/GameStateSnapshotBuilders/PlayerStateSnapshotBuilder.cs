using Catan.Core.Models;
using Catan.Core.Runtime;
using Catan.Core.Snapshots.ClientQueries;

namespace Catan.Core.Queries.GameStateSnapshotBuilders
{
    public class PlayerStateSnapshotBuilder
    {
        private readonly GameSession _session;

        public PlayerStateSnapshotBuilder(GameSession session)
        {
            _session = session;
        }

        public List<FullPlayerSnapshot> GetAllFullPlayerData()
        {
            var playerDataList = new List<FullPlayerSnapshot>();
            
            foreach (var player in _session.GetAllPlayersView())
            {
                playerDataList.Add(GetFullPlayerData(player));
            }

            return playerDataList;
        }

        public FullPlayerSnapshot GetFullPlayerDataFromId(int playerId) => _session.GetFullPlayerData(playerId);

        private FullPlayerSnapshot GetFullPlayerData(Player player) => _session.GetFullPlayerData(player.ID);

        public OtherPlayersSnapshot GetOtherPlayersData(int playerId) => _session.GetOtherPlayersData(playerId);
    }
}