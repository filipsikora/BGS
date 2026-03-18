namespace Catan.Shared.Data
{
    public static class BuildingDataRegistry
    {
        public static readonly Dictionary<EnumBuildings, int> MaxPerPlayer = new()
        {
            { EnumBuildings.Village, 5 },
            { EnumBuildings.Town, 4 },
            { EnumBuildings.Road, 15 }
        };

        public static readonly Dictionary<EnumBuildings, string> Name = new()
        {
            { EnumBuildings.Village, "Village" },
            { EnumBuildings.Town, "Town" },
            { EnumBuildings.Road, "Road" }
        };

        public static readonly Dictionary<EnumBuildings, int> Worth = new()
        {
            { EnumBuildings.Village, 1 },
            { EnumBuildings.Town, 2 },
            { EnumBuildings.Road, 0 } 
        };
    }
}