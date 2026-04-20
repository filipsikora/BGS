using Catan.Core.Conditions;
using Catan.Core.Models;
using Catan.Core.Results;
using Catan.Core.Data;

namespace Catan.Core.Rules
{
    public static class RulesBuilding
    {
        public static ResultCondition CanBuildInitialVillage(Player player, Vertex vertex, GameSession session)
        {
            return ResultCondition.Combine(
                ConditionsMap.PositionExists(vertex.Id, id => session.GetVertexById(id)),
                ConditionsBuildings.HasAvailable(EnumBuildings.Village, player),
                ConditionsMap.IsNotOwned(vertex),
                ConditionsBuildings.NoSettlementsInRange(vertex),
                ConditionsBuildings.InitialVillagePlaced(session.GetVillagePlacedThisTurn()),
                ConditionsTurn.IsInitialRound(session.CheckIfIsInitialRound())
                );
        }

        public static ResultCondition CanBuildInitialRoad(Player player, Edge edge, Vertex vertex, GameSession session)
        {
            return ResultCondition.Combine(
                ConditionsMap.PositionExists(edge.Id, id => session.GetEdgeById(id)),
                ConditionsBuildings.HasAvailable(EnumBuildings.Road, player),
                ConditionsMap.IsNotOwned(edge),
                ConditionsMap.HasAccessToPosition(player, edge),
                ConditionsBuildings.AdjacentToLastVillage(edge, vertex),
                ConditionsBuildings.InitialVillagePlaced(!session.GetVillagePlacedThisTurn()),
                ConditionsBuildings.InitialRoadPlaced(session.GetRoadPlacedThisTurn()),
                ConditionsTurn.IsInitialRound(session.CheckIfIsInitialRound()));
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