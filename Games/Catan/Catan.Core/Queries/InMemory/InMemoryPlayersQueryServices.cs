using Catan.Core.Queries.Interfaces;
using Catan.Core.Runtime;
using Catan.Core.Snapshots.ClientQueries;

namespace Catan.Core.Queries.InMemory
{
    public sealed class InMemoryPlayersQueryServices : IPlayersQueryService
    {
        private readonly GameSession _session;

        public InMemoryPlayersQueryServices(GameSession session)
        {
            _session = session;
        }

        public PlayerResourcesSnapshot GetPlayersCards(int playerId) => _session.GetPlayerssResourcesData(playerId);

        public PlayerDataSnapshot GetPlayersData(int playerId) => _session.GetPlayerData(playerId);

        public CurrentPlayerIdSnapshot GetCurrentPlayerId()
        {
            var currentPlayerId = _session.GetCurrentPlayerId();

            return new CurrentPlayerIdSnapshot(currentPlayerId);
        }

        public List<PlayerNameSnapshot> GetAllPlayersNames() => _session.GetAllPlayersNamesData();

        public List<PlayerNameSnapshot> GetSomePlayersNames(List<int> playersIds) => _session.GetSomePlayersNamesData(playersIds);

        public List<PlayerNameSnapshot> GetNotCurrentPlayersNames() => _session.GetNotCurrentPlayerNamesData();

        public PlayerResourcesSnapshot GetVictimsCards() => _session.GetVictimCardsData();

        public FullPlayerSnapshot GetFullPlayerData(int playerId) => _session.GetFullPlayerData(playerId);
    }
}