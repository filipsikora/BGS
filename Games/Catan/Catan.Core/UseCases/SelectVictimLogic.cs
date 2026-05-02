using Catan.Core.Results;
using Catan.Core.Rules;
using Catan.Shared.Data;

namespace Catan.Core.UseCases
{
    public sealed class SelectVictimLogic : BaseUseCase
    {
        public SelectVictimLogic(GameSession session) : base(session) { }

        public ResultCondition Handle(int victimId)
        {
            var victim = Session.GetPlayerById(victimId);
            var possibleVictimsIds = Session.GetPossibleVictimsIds();
            var validation = RulesRobber.ValidVictim(victim, possibleVictimsIds);

            if (!validation.Success)
            {
                return ResultCondition.Fail(validation.Reason);
            }

            Session.CreateCardsStealingContext(victim.ID);

            return ApplyPhase(ResultCondition.Ok(EnumGamePhases.CardStealing));
        }
    }
}