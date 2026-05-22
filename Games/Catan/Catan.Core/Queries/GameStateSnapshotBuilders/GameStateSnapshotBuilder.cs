using Catan.Core.Runtime;
using Catan.Core.Snapshots.ClientQueries;
using Catan.Core.Snapshots.Persistence;

namespace Catan.Core.Queries.GameStateSnapshotBuilders
{
    public class GameStateSnapshotBuilder
    {
        private readonly GameSession _session;

        private readonly BoardStateSnapshotBuilder BoardBuilder;
        private readonly GameFlowSnapshotBuilder GameFlowBuilder;

        public GameStateSnapshotBuilder(GameSession session)
        {
            _session = session;

            BoardBuilder = new BoardStateSnapshotBuilder(_session);
            GameFlowBuilder = new GameFlowSnapshotBuilder(_session);
        }

        public GameStateSnapshot GetGameStateData()
        {
            return new GameStateSnapshot(GetFullBoardData(), GetFullGameFlowData(), )
        }

        private FullBoardSnapshot GetFullBoardData() => BoardBuilder.GetFullBoardData();
        private FullGameFlowSnapshot GetFullGameFlowData() => GameFlowBuilder.GetFullGameFloweData();
    }
}