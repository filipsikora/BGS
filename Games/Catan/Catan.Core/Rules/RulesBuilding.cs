using Catan.Core.Conditions;
using Catan.Core.Models;
using Catan.Core.Results;
using Catan.Core.Data;
using Catan.Shared.Data;

namespace Catan.Core.Rules
{
    public static class RulesBuilding
    {
        public static ResultCondition CanBuildInitialVillage(Player player, int vertexId, GameSession session)
        {
            var exists = ConditionsMap.VertexExists(vertexId, session);
            if (!exists.Success)
                return exists;

            var vertex = session.GetVertexById(vertexId);

            return ResultCondition.Combine(
                ConditionsBuildings.HasAvailable(EnumBuildings.Village, player),
                ConditionsMap.IsNotOwned(vertex),
                ConditionsBuildings.NoSettlementsInRange(vertex),
                ConditionsBuildings.InitialVillagePlaced(session.GetVillagePlacedThisTurn()),
                ConditionsTurn.IsCorrectPhase(EnumGamePhases.FirstRoundsBuilding, session)
                );
        }

        public static ResultCondition CanBuildInitialRoad(Player player, int edgeId, int vertexId, GameSession session)
        {
            var vertexExists = ConditionsMap.VertexExists(vertexId, session);
            if (!vertexExists.Success)
                return vertexExists;

            var edgeExists = ConditionsMap.EdgeExists(edgeId, session);
            if (!edgeExists.Success)
                return edgeExists;

            var vertex = session.GetVertexById(vertexId);
            var edge = session.GetEdgeById(edgeId);

            return ResultCondition.Combine(
                ConditionsBuildings.HasAvailable(EnumBuildings.Road, player),
                ConditionsMap.IsNotOwned(edge),
                ConditionsMap.HasAccessToPosition(player, edge),
                ConditionsBuildings.AdjacentToLastVillage(edge, vertex),
                ConditionsBuildings.InitialVillagePlaced(!session.GetVillagePlacedThisTurn()),
                ConditionsBuildings.InitialRoadPlaced(session.GetRoadPlacedThisTurn()),
                ConditionsTurn.IsCorrectPhase(EnumGamePhases.FirstRoundsBuilding, session)
                );
        }

        public static ResultCondition CanBuildVillage(Player player, int vertexId, GameSession session)
        {
            var exists = ConditionsMap.VertexExists(vertexId, session);
            if (!exists.Success)
                return exists;

            var vertex = session.GetVertexById(vertexId);

            return ResultCondition.Combine(
                ConditionsResources.CanAfford(player.Resources, BuildingVillage.Cost),
                ConditionsBuildings.HasAvailable(EnumBuildings.Village, player),
                ConditionsMap.IsNotOwned(vertex),
                ConditionsBuildings.NoSettlementsInRange(vertex),
                ConditionsMap.HasAccessToPosition(player, vertex));
        }

        public static ResultCondition CanBuildRoad(Player player, int edgeId, GameSession session)
        {
            var exists = ConditionsMap.EdgeExists(edgeId, session);
            if (!exists.Success)
                return exists;

            var edge = session.GetEdgeById(edgeId);

            return ResultCondition.Combine(
                ConditionsResources.CanAfford(player.Resources, BuildingRoad.Cost),
                ConditionsBuildings.HasAvailable(EnumBuildings.Road, player),
                ConditionsMap.IsNotOwned(edge),
                ConditionsMap.HasAccessToPosition(player, edge));
        }

        public static ResultCondition CanUpgradeVillage(Player player, int vertexId, GameSession session)
        {
            var exists = ConditionsMap.VertexExists(vertexId, session);
            if (!exists.Success)
                return exists;

            var vertex = session.GetVertexById(vertexId);

            return ResultCondition.Combine(
                ConditionsResources.CanAfford(player.Resources, BuildingTown.Cost),
                ConditionsBuildings.HasAvailable(EnumBuildings.Town, player),
                ConditionsBuildings.HasVillage(player, vertex));
        }

        public static ResultCondition CanBuildFreeRoad(Player player, int edgeId, GameSession session)
        {
            var exists = ConditionsMap.EdgeExists(edgeId, session);
            if (!exists.Success)
                return exists;

            var edge = session.GetEdgeById(edgeId);

            return ResultCondition.Combine(
                ConditionsBuildings.HasAvailable(EnumBuildings.Road, player),
                ConditionsMap.IsNotOwned(edge),
                ConditionsMap.HasAccessToPosition(player, edge),
                ConditionsTurn.IsCorrectPhase(EnumGamePhases.RoadBuilding, session));
        }
    }
}