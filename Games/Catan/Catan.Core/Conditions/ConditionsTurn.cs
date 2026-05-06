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

        public static ResultCondition IsEitherPhaseCorrect(List<EnumGamePhases> phases, GameSession session)
        {
            foreach (var phase in phases)
            {
                if (session.CheckIfIsCorePhase(phase))
                {
                    return ResultCondition.Ok();
                }
            }

            return ResultCondition.Fail(ConditionFailureReason.NotCorrectPhase);
        }
    }
}