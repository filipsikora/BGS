#nullable enable
using Catan.Core.Data;
using Catan.Core.Helpers;

namespace Catan.Core.Models
{
    public class Player
    {
        public string? Name { get; set; } = null;

        public int Points { get; set; } = 0;

        public List<Building> Buildings = new();

        public List<int> DevelopmentCardsByID = new();

        public int KnightsUsed = 0;

        public int VictoryPointsCardsUsed = 0;

        public int LongestRoadCount = 0;

        public int ExtraPoints = 0;

        public int ID;

        public ResourceCostOrStock Resources { get; set; } = new ResourceCostOrStock();

        public List<Port> Ports { get; set; } = new();


        public Player(string? name, int id)
        {
            Name = name;
            ID = id;
            Resources = new ResourceCostOrStock() { Name = name };
        }

        public int BuildingCount(EnumBuildings type)
        {
            return Buildings.Count(b => BuildingsMapper.ToBuildingType(b.GetType()) == type);
        }

        public int CountPoints()
        {
            int villagesPoints = BuildingCount(EnumBuildings.Village);
            int townPoints = BuildingCount(EnumBuildings.Town) * 2;
            int points = villagesPoints + townPoints + VictoryPointsCardsUsed + ExtraPoints;
            Points = points;
            
            return points;
        }

        public override string ToString()
        {
            return Name ?? "Unnamed Player";
        }
    }
}
