using Catan.Core.Snapshots.ClientQueries;

namespace Catan.Core.Snapshots.Persistence
{
    public sealed class GameStateSnapshot
    {
        public FullBoardSnapshot Board;
        public FullGameFlowSnapshot GameFlow;

        public List<FullPlayerSnapshot> Players;

        public FullBankSnapshot Bank;
        public FullPhaseContextSnapshot PhaseContext;

        public GameStateSnapshot(FullBoardSnapshot board, FullGameFlowSnapshot gameFlow, List<FullPlayerSnapshot> players, FullBankSnapshot bank, FullPhaseContextSnapshot phaseContext)
        {
            Board = board;
            GameFlow = gameFlow;
            Players = players;
            Bank = bank;
            PhaseContext = phaseContext;
        }
    }
}