using Catan.Core.Conditions;
using Catan.Core.Engine;
using Catan.Core.Models;
using Catan.Core.Results;

namespace Catan.Core.Rules
{
    public static class RulesRobber
    {
        public static ResultCondition CanSteal(Player victim, CardStealingContext context)
        {
            return ResultCondition.Combine(
                ConditionsRobber.StealContextIsValid(context, victim.ID),
                ConditionsResources.HasAnyResources(victim),
                ConditionsPlayer.PlayerExists(victim));
        }

        public static ResultCondition CanBlock(int hexId, GameSession session)
        {
            var exists = ConditionsMap.HexExists(hexId, session);
            if (!exists.Success)
                return exists;

            var hex = session.GetHexById(hexId);

            return ConditionsMap.IsNotBlocked(hex);
        }

        public static ResultCondition ValidVictim(Player victim, List<int> possibleVictimsIds)
        {
            return ResultCondition.Combine(
                ConditionsPlayer.PlayerExists(victim),
                ConditionsRobber.VictimPossible(possibleVictimsIds, victim));
        }
    }
}