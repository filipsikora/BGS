using Catan.Core.Conditions;
using Catan.Core.Results;

namespace Catan.Core.Rules
{
    public static class RulesTurn
    {
        public static ResultCondition CanFinishInitialTurn(GameSession session, bool initialRound)
        {
            return ResultCondition.Combine(
                ConditionsTurn.IsInitialRound(initialRound),
                ConditionsBuildings.InitialVillagePlaced(!session.GetVillagePlacedThisTurn()),
                ConditionsBuildings.InitialRoadPlaced(!session.GetRoadPlacedThisTurn()));
        }
    }
}