using Catan.Core.Engine;
using Catan.Core.Interfaces;
using Catan.Core.Models;
using Catan.Core.Results;
using Catan.Shared.Data;

namespace Catan.Core.Conditions
{
    public static class ConditionsMap
    {
        public static ResultCondition MapExists(HexMap map)
        {
            if (map == null)
            {
                return ResultCondition.Fail(ConditionFailureReason.DoesNotExist);
            }

            return ResultCondition.Ok();
        }

        public static ResultCondition VertexExists(int vertexId, GameSession session)
        {
            return session.TryGetVertexById(vertexId) ? ResultCondition.Ok() : ResultCondition.Fail(ConditionFailureReason.DoesNotExist);
        }

        public static ResultCondition EdgeExists(int edgeId, GameSession session)
        {
            return session.TryGetEdgeById(edgeId) ? ResultCondition.Ok() : ResultCondition.Fail(ConditionFailureReason.DoesNotExist);
        }

        public static ResultCondition HexExists(int hexId, GameSession session)
        {
            return session.TryGetHexById(hexId) ? ResultCondition.Ok() : ResultCondition.Fail(ConditionFailureReason.DoesNotExist);
        }

        public static ResultCondition IsNotBlocked(HexTile hex)
        {
            if (hex.isBlocked)
            {
                return ResultCondition.Fail(ConditionFailureReason.HexAlreadyBlocked);
            }

            return ResultCondition.Ok();
        }

        public static ResultCondition IsNotOwned(IPositionData position)
        {
            if (position.Owner != null)
            {
                return ResultCondition.Fail(ConditionFailureReason.PositionOccupied);
            }

            return ResultCondition.Ok();
        }

        public static ResultCondition HasAccessToPosition(Player player, IPositionData position)
        {
            if (position.AccessibleByPlayer(player))
            {
                return ResultCondition.Ok();
            }

            return ResultCondition.Fail(ConditionFailureReason.NoAccess);
        }
    }
}