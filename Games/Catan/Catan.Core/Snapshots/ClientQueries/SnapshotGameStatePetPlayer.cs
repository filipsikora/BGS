namespace Catan.Core.Snapshots.ClientQueries
{
    public sealed class GameStatePerPlayerSnapshot
    {
        public FullBoardSnapshot Board;
        public FullGameFlowSnapshot GameFlow;

        public PlayerResourcesSnapshot Resources;
        public PlayerDataSnapshot Data;

        public GameStatePerPlayerSnapshot(FullBoardSnapshot board, FullGameFlowSnapshot gameFlow, PlayerResourcesSnapshot resources, PlayerDataSnapshot data)
        {
            Board = board;
            GameFlow = gameFlow;
            Resources = resources;
            Data = data;
        }
    }
}