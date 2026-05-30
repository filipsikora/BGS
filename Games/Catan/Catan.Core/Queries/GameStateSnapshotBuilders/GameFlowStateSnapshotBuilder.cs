using Catan.Core.Runtime;
using Catan.Core.Snapshots.ClientQueries;

namespace Catan.Core.Queries.GameStateSnapshotBuilders
{
    public class GameFlowStateSnapshotBuilder
    {
        private readonly GameSession _session;

        public GameFlowStateSnapshotBuilder(GameSession session)
        {
            _session = session;
        }

        public FullGameFlowSnapshot GetFullGameFloweData()
        {
            return new FullGameFlowSnapshot(
                _session.GetTurn(),
                _session.GetLastRoll(),
                _session.GetCurrentPlayerId(),
                _session.GetKnightChampionId(),
                _session.GetRoadChampionId(),
                _session.GetCurrentCorePhase()
                );
        }

        public FullBankSnapshot GetFullBankData()
        {
            return new FullBankSnapshot(
                _session.GetBank().ToDictionary(),
                _session.GetDevCardsInBankData()
                );
        }
    }
}
