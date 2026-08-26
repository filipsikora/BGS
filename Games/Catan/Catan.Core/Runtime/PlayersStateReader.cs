using Catan.Core.Data;
using Catan.Core.Models;
using Catan.Core.Snapshots.ClientQueries;

namespace Catan.Core.Runtime
{
    public sealed class PlayersStateReader
    {
        public PlayersStateReader() { }

        public PlayerResourcesSnapshot GetPlayersCards(Player player)
        {
            return new PlayerResourcesSnapshot(player.Resources.ToDictionary());
        }

        public PlayerDataSnapshot GetPlayersData(Player player)
        {
            var playerBuildingsLeft = new Dictionary<string, int>();

            foreach (var buildingType in BuildingDataRegistry.MaxPerPlayer.Keys)
            {
                int maxAvailable = BuildingDataRegistry.MaxPerPlayer[buildingType];
                int playerUsed = player.BuildingCount(buildingType);
                int playerLeft = maxAvailable - playerUsed;

                playerBuildingsLeft.Add(BuildingDataRegistry.Name[buildingType], playerLeft);
            }

            return new PlayerDataSnapshot(player.Name, playerBuildingsLeft, player.Points, player.KnightsUsed, player.VictoryPointsCardsUsed, player.ExtraPoints);
        }

        public List<PlayerNameSnapshot> GetAllPlayersNames(List<Player> playerList)
        {
            var allPlayersNamesList = new List<PlayerNameSnapshot>();

            foreach (var player in playerList)
            {
                var playerNameData = new PlayerNameSnapshot(player.ID, player.Name);

                allPlayersNamesList.Add(playerNameData);
            }

            return allPlayersNamesList;
        }

        public List<PlayerNameSnapshot> GetSomePlayersNames(List<Player> players)
        {
            var playersData = new List<PlayerNameSnapshot>();

            foreach (var player in players)
            {
                var playerNameData = new PlayerNameSnapshot(player.ID, player.Name);

                playersData.Add(playerNameData);
            }

            return playersData;
        }

        public List<PlayerNameSnapshot> GetNotCurrentPlayersNames(IEnumerable<Player> players)
        {
            var playersData = new List<PlayerNameSnapshot>();

            foreach (var player in players)
            {
                var playerData = new PlayerNameSnapshot(player.ID, player.Name);
                playersData.Add(playerData);    
            }

            return playersData;
        }

        public PlayerResourcesSnapshot GetVictimsCards(Player victim)
        {
            var victimCards = GetPlayersCards(victim);

            return victimCards;
        }

        public FullPlayerSnapshot GetFullPlayerData(Player player, IReadOnlyList<DevelopmentCardSnapshot> playerDevCards)
        {
            return new FullPlayerSnapshot(
                GetPlayersCards(player),
                new FullPlayerDataSnapshot(player.Name, player.ID, GetPlayerBuildingsLeft(player), player.Points, player.KnightsUsed, player.VictoryPointsCardsUsed, player.ExtraPoints, playerDevCards,
                player.DevelopmentCardsByID.Count, player.Resources.Total(), player.VictoryPointsCardsUsed, player.KnightsUsed)
                );
        }

        public BasicPlayerSnapshot GetBasicPlayerData(Player player)
        {
            return new BasicPlayerSnapshot(
                player.ID, player.Name, player.Points, player.ExtraPoints, player.Resources.ResourceDictionary.Count, player.DevelopmentCardsByID.Count, player.VictoryPointsCardsUsed, player.KnightsUsed, 
                GetPlayerBuildingsLeft(player)
                );
        }

        public OtherPlayersSnapshot GetOtherPlayersData(IEnumerable<Player> otherPlayers)
        {
            return new OtherPlayersSnapshot(otherPlayers.Select(GetBasicPlayerData).ToList());
        }

        private Dictionary<string, int> GetPlayerBuildingsLeft(Player player)
        {
            var playerBuildingsLeft = new Dictionary<string, int>();

            foreach (var buildingType in BuildingDataRegistry.MaxPerPlayer.Keys)
            {
                int maxAvailable = BuildingDataRegistry.MaxPerPlayer[buildingType];
                int playerUsed = player.BuildingCount(buildingType);
                int playerLeft = maxAvailable - playerUsed;
                playerBuildingsLeft.Add(BuildingDataRegistry.Name[buildingType], playerLeft);
            }

            return playerBuildingsLeft;
        }
    }
}