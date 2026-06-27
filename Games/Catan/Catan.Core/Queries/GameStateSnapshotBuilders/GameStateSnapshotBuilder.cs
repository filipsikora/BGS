using Catan.Core.Runtime;
using Catan.Core.Snapshots.ClientQueries;
using Catan.Core.Snapshots.Persistence;

namespace Catan.Core.Queries.GameStateSnapshotBuilders
{
    public class GameStateSnapshotBuilder
    {
        private readonly GameSession _session;

        private readonly BoardStateSnapshotBuilder BoardBuilder;
        private readonly GameFlowStateSnapshotBuilder GameFlowBuilder;
        private readonly PlayerStateSnapshotBuilder PlayerBuilder;
        private readonly ContextStateSnapshotBuilder ContextBuilder;

        public GameStateSnapshotBuilder(GameSession session)
        {
            _session = session;

            BoardBuilder = new BoardStateSnapshotBuilder(_session);
            GameFlowBuilder = new GameFlowStateSnapshotBuilder(_session);
            PlayerBuilder = new PlayerStateSnapshotBuilder(_session);
            ContextBuilder = new ContextStateSnapshotBuilder(_session);
        }

        public GameStateSnapshot GetGameStateData()
        {
            return new GameStateSnapshot(GetFullBoardData(), GetFullGameFlowData(), GetAllFullPlayerData(), GetFullBankData(), GetFullPhaseData());
        }

        public GameStatePerPlayerSnapshot GetGameStatePerPlayerData(int playerId)
        {
            return new GameStatePerPlayerSnapshot(GetFullBoardData(), GetFullGameFlowData(), GetFullPlayerDataPerId(playerId), GetOtherPlayersData(playerId));
        }

        private FullBoardSnapshot GetFullBoardData() => BoardBuilder.GetFullBoardData();
        private FullGameFlowSnapshot GetFullGameFlowData() => GameFlowBuilder.GetFullGameFloweData();
        private List<FullPlayerSnapshot> GetAllFullPlayerData() => PlayerBuilder.GetAllFullPlayerData();
        private OtherPlayersSnapshot GetOtherPlayersData(int playerId) => PlayerBuilder.GetOtherPlayersData(playerId);
        private FullPlayerSnapshot GetFullPlayerDataPerId(int playerId) => PlayerBuilder.GetFullPlayerDataFromId(playerId);
        private FullBankSnapshot GetFullBankData() => GameFlowBuilder.GetFullBankData();
        private FullPhaseContextSnapshot GetFullPhaseData() => ContextBuilder.GetFullPhaseContextData();
    }
}