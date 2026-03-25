using Catan.Core.Models;
using Catan.Core.Data;

namespace Catan.Core.Helpers
{
    public static class BuildingsMapper
    {
        public static EnumBuildings ToBuildingType(Type type)
        {
            if (type == typeof(BuildingVillage))
                return EnumBuildings.Village;

            if (type == typeof(BuildingRoad))
                return EnumBuildings.Road;

            if (type == typeof(BuildingTown))
                return EnumBuildings.Town;

            throw new InvalidOperationException($"Unknown type: {type}");
        }
    }
}