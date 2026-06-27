using Catan.Core.Snapshots.ClientQueries;

namespace Catan.Core.Snapshots.Persistence
{
    public sealed class GameStatePerPlayerSnapshot
    {
        public FullBoardSnapshot Board;
        public FullGameFlowSnapshot GameFlow;

        public OtherPlayersSnapshot OtherPlayers;

        public FullPlayerSnapshot Player;

        public GameStatePerPlayerSnapshot(FullBoardSnapshot board, FullGameFlowSnapshot gameFlow, FullPlayerSnapshot player, OtherPlayersSnapshot otherPlayers)
        {
            Board = board;
            GameFlow = gameFlow;
            Player = player;
            OtherPlayers = otherPlayers;
        }
    }
}