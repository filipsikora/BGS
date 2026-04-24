using Catan.Core.Results;
using Catan.Shared.Data;

namespace Catan.Core.Conditions
{
    public static class ConditionsTurn
    {
        public static ResultCondition IsCorrectPhase(EnumGamePhases phase, GameSession session)
        {
            if (session.CheckIfIsCorePhase(phase))
            {
                return ResultCondition.Ok();
            }

            return ResultCondition.Fail(ConditionFailureReason.NotCorrectPhase);
        }

        internal static bool IsCorrectPhase(object firstRoundsBuilding, GameSession session)
        {
            throw new NotImplementedException();
        }
    }
}