using Catan.Core.Conditions;
using Catan.Core.Models;
using Catan.Core.Results;
using Catan.Shared.Data;

namespace Catan.Core.Rules
{
    public static class RulesBuilding
    {
        public static ResultCondition CanBuildInitialVillage(Player player, Vertex vertex, GameSession session)
        {
            if (player == null) throw new Exception("Player is null");
            if (vertex == null) throw new Exception("Vertex is null");

            if (session == null) throw new Exception("Session is null");
            return ResultCondition.Combine(
                ConditionsMap.PositionExists(vertex.Id, id => session.GetVertexById(id)),
                ConditionsBuildings.HasAvailable(EnumBuildings.Village, player),
                ConditionsMap.IsNotOwned(vertex),
                ConditionsBuildings.NoSettlementsInRange(vertex));
        }

        public static ResultCondition CanBuildInitialRoad(Player player, Edge edge, Vertex vertex, GameSession session)
        {
            return ResultCondition.Combine(
                ConditionsMap.PositionExists(edge.Id, id => session.GetEdgeById(id)),
                ConditionsBuildings.HasAvailable(EnumBuildings.Road, player),
                ConditionsMap.IsNotOwned(edge),
                ConditionsMap.HasAccessToPosition(player, edge),
                ConditionsBuildings.AdjacentToLastVillage(edge, vertex));
        }

        public static ResultCondition CanBuildVillage(Player player, Vertex vertex, GameSession session)
        {
            return ResultCondition.Combine(
                ConditionsMap.PositionExists(vertex.Id, id => session.GetVertexById(id)),
                ConditionsResources.CanAfford(player.Resources, BuildingVillage.Cost),
                ConditionsBuildings.HasAvailable(EnumBuildings.Village, player),
                ConditionsMap.IsNotOwned(vertex),
                ConditionsBuildings.NoSettlementsInRange(vertex),
                ConditionsMap.HasAccessToPosition(player, vertex));
        }

        public static ResultCondition CanBuildRoad(Player player, Edge edge, GameSession session)
        {
            return ResultCondition.Combine(
                ConditionsMap.PositionExists(edge.Id, id => session.GetEdgeById(id)),
                ConditionsResources.CanAfford(player.Resources, BuildingRoad.Cost),
                ConditionsBuildings.HasAvailable(EnumBuildings.Road, player),
                ConditionsMap.IsNotOwned(edge),
                ConditionsMap.HasAccessToPosition(player, edge));
        }

        public static ResultCondition CanUpgradeVillage(Player player, Vertex vertex, GameSession session)
        {
            return ResultCondition.Combine(
                ConditionsMap.PositionExists(vertex.Id, id => session.GetVertexById(id)),
                ConditionsResources.CanAfford(player.Resources, BuildingTown.Cost),
                ConditionsBuildings.HasAvailable(EnumBuildings.Town, player),
                ConditionsBuildings.HasVillage(player, vertex));
        }

        public static ResultCondition CanBuildFreeRoad(Player player, Edge edge, GameSession session)
        {
            return ResultCondition.Combine(
                ConditionsMap.PositionExists(edge.Id, id => session.GetEdgeById(id)),
                ConditionsBuildings.HasAvailable(EnumBuildings.Road, player),
                ConditionsMap.IsNotOwned(edge),
                ConditionsMap.HasAccessToPosition(player, edge));
        }
    }
}