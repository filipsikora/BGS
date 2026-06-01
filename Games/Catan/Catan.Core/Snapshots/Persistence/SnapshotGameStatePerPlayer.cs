using Catan.Core.Snapshots.ClientQueries;

namespace Catan.Core.Snapshots.Persistence
{
    public sealed class GameStatePerPlayerSnapshot
    {
        public FullBoardSnapshot Board;
        public FullGameFlowSnapshot GameFlow;

        public PlayerResourcesSnapshot Resources;
        public FullPlayerSnapshot Player;

        public GameStatePerPlayerSnapshot(FullBoardSnapshot board, FullGameFlowSnapshot gameFlow, FullPlayerSnapshot player)
        {
            Board = board;
            GameFlow = gameFlow;
            Player = player;
        }
    }
}