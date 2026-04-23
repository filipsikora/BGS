using Catan.Core.Conditions;
using Catan.Core.Models;
using Catan.Core.Results;

namespace Catan.Core.Rules
{
    public static class RulesPlacement
    {
        public static ResultCondition CanPlaceRoad(int edgeId, GameSession session)
        {
            var exists = ConditionsMap.EdgeExists(edgeId, session);

            if (!exists.Success)
                return exists;

            var edge = session.GetEdgeById(edgeId);

            return ConditionsMap.IsNotOwned(edge);
        }

        public static ResultCondition CanPlaceVillage(int vertexId, GameSession session)
        {
            var exists = ConditionsMap.VertexExists(vertexId, session);

            if (!exists.Success)
                return exists;

            var vertex = session.GetVertexById(vertexId);

            return ConditionsMap.IsNotOwned(vertex);
        }

        public static ResultCondition CanPlaceTown(Player player, int vertexId, GameSession session)
        {
            var exists = ConditionsMap.VertexExists(vertexId, session);

            if (!exists.Success)
                return exists;

            var vertex = session.GetVertexById(vertexId);

            return ConditionsBuildings.HasVillage(player, vertex);
        }
    }
}
