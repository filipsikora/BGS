using Catan.Shared.Data;
using System.Collections.Generic;

namespace Catan.Core.Snapshots.ClientQueries
{
    public sealed class FullPlayerSnapshot
    {
        public PlayerResourcesSnapshot Resources;
        public FullPlayerDataSnapshot Data;

        public FullPlayerSnapshot(PlayerResourcesSnapshot resources, FullPlayerDataSnapshot data)
        {
            Resources = resources;
            Data = data;
        }
    }

    public sealed class FullPlayerDataSnapshot
    {
        public string Name;

        public Dictionary<string, int> BuildingsLeft;

        public int Points;
        public int Knights;
        public int VictoryPoints;
        public int ExtraPoints;

        public IReadOnlyList<DevelopmentCardSnapshot> DevCards;

        public FullPlayerDataSnapshot(string name, Dictionary<string, int> buildingsLeft, int points, int knights, int victoryPoints, int extraPoints, IReadOnlyList<DevelopmentCardSnapshot> devCards)
        {
            Name = name;
            BuildingsLeft = buildingsLeft;
            Points = points;
            Knights = knights;
            VictoryPoints = victoryPoints;
            ExtraPoints = extraPoints;
            DevCards = devCards;
        }
    }

    public sealed class PlayerResourcesSnapshot
    {
        public Dictionary<EnumResourceType, int> PlayerResources;
        public PlayerResourcesSnapshot(Dictionary<EnumResourceType, int> playerResources)
        {
            PlayerResources = playerResources;
        }
    }

    public sealed class PlayerDataSnapshot
    {
        public string Name;

        public Dictionary<string, int> BuildingsLeft;

        public int Points;
        public int Knights;
        public int VictoryPoints;
        public int ExtraPoints;

        public PlayerDataSnapshot(string name, Dictionary<string, int> buildingsLeft, int points, int knights, int victoryPoints, int extraPoints)
        {
            Name = name;
            BuildingsLeft = buildingsLeft;
            Points = points;
            Knights = knights;
            VictoryPoints = victoryPoints;
            ExtraPoints = extraPoints;
        }
    }

    public sealed class CurrentPlayerIdSnapshot
    {
        public int CurrentPlayerId;

        public CurrentPlayerIdSnapshot(int currentPlayerId)
        {
            CurrentPlayerId = currentPlayerId;
        }
    }

    public sealed class PlayerNameSnapshot
    {
        public int Id;
        public string Name;

        public PlayerNameSnapshot(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
