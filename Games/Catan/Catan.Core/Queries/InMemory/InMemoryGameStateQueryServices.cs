using Catan.Core.Data;
using Catan.Core.Queries.Interfaces;
using Catan.Core.Snapshots;

namespace Catan.Core.Queries.InMemory
{
    public sealed class InMemoryGameStateQueryServices : IGameStateQueryService
    {
        private readonly GameSession _session;
        private readonly InMemoryDevCardQueryService _devCardsQuery;

        public InMemoryGameStateQueryServices(GameSession session, InMemoryDevCardQueryService devCardsQuery)
        {
            _session = session;
            _devCardsQuery = devCardsQuery;
        }

        public FullPlayerDataSnapshot GetFullPlayerData(int playerId)
        {
            var player = _session.GetPlayerById(playerId);
            var playerBuildingsLeft = new Dictionary<string, int>();
            var afterRoll = _session.GetAfterRoll();

            var playerDevCards = _devCardsQuery.GetPlayerDevCardsById(playerId);

            foreach (var buildingType in BuildingDataRegistry.MaxPerPlayer.Keys)
            {
                int maxAvailable = BuildingDataRegistry.MaxPerPlayer[buildingType];
                int playerUsed = player.BuildingCount(buildingType);
                int playerLeft = maxAvailable - playerUsed;

                playerBuildingsLeft.Add(BuildingDataRegistry.Name[buildingType], playerLeft);
            }

            return new FullPlayerDataSnapshot(player.Name, playerBuildingsLeft, player.Points, player.KnightsUsed, player.VictoryPointsCardsUsed, player.ExtraPoints, playerDevCards);
        }
    }
}
