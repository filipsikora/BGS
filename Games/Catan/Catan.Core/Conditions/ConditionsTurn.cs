using Catan.Core.Results;
using Catan.Shared.Data;

namespace Catan.Core.Conditions
{
    public static class ConditionsTurn
    {
        public static ResultCondition IsInitialRound(bool initialRound)
        {
            if (!initialRound)
            {
                return ResultCondition.Fail(ConditionFailureReason.NotInitialRound);
            }

            return ResultCondition.Ok();
        }
    }
}
