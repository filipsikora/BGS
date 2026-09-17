using Catan.Core.Engine;
using Catan.Core.Models;
using Catan.Core.Results;
using Catan.Shared.Data;

namespace Catan.Core.Conditions
{
    public class ConditionsRobber
    {
        public static ResultCondition StealContextIsValid(CardStealingContext context, int thiefId, int victimId)
        {
            if (context == null)
            {
                return ResultCondition.Fail(ConditionFailureReason.DoesNotExist);
            }

            if (context.VictimId != victimId)
            {
                return ResultCondition.Fail(ConditionFailureReason.VictimInvalid);
            }

            if (context.TheifId != thiefId)
            {
                return ResultCondition.Fail(ConditionFailureReason.ThiefInvalid);
            }

            return ResultCondition.Ok();
        }

        public static ResultCondition VictimPossible(List<int> possibleVictimsIds, Player victim)
        {
            if (possibleVictimsIds.Contains(victim.ID))
            {
                return ResultCondition.Ok();
            }

            return ResultCondition.Fail(ConditionFailureReason.VictimInvalid);
        }
    }
}