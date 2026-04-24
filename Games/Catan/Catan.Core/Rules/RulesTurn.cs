using Catan.Core.Conditions;
using Catan.Core.Results;
using Catan.Shared.Data;

namespace Catan.Core.Rules
{
    public static class RulesTurn
    {
        public static ResultCondition CanFinishInitialTurn(GameSession session, bool initialRound)
        {
            return ResultCondition.Combine(
                ConditionsTurn.IsCorrectPhase(EnumGamePhases.FirstRoundsBuilding, session),
                ConditionsBuildings.InitialVillagePlaced(!session.GetVillagePlacedThisTurn()),
                ConditionsBuildings.InitialRoadPlaced(!session.GetRoadPlacedThisTurn()));
        }
    }
}