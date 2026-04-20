using Catan.Core.DomainEvents;
using Catan.Core.Results;
using Catan.Core.Rules;
using Catan.Shared.Data;

namespace Catan.Core.PhaseLogic
{
    public sealed class StealCardLogic : BaseLogic
    {
        public StealCardLogic(GameSession session) : base(session) { }

        public  ResultStealResource Handle(int victimId, EnumResourceType resource)
        {
            var thief = Session.GetCurrentPlayer();
            var victim = Session.GetPlayerById(victimId);

            var (exists, context) = Session.TryGetCardStealingContext();

            if (!exists)
                return ResultStealResource.Fail(thief.ID, victim.ID, ConditionFailureReason.DoesNotExist);

            var validation = RulesRobber.CanSteal(victim, context);

            if (!validation.Success)
            {
                return ResultStealResource.Fail(thief.ID, victimId, validation.Reason);
            }

            Session.CardStolenMutation(victim, resource);

            var afterRoll = Session.GetAfterRoll();

            var nextPhase = afterRoll ? EnumGamePhases.NormalRound : EnumGamePhases.BeforeRoll;
            
            var result = ResultStealResource.Ok(thief.ID, victimId, resource, nextPhase);
            result.AddDomainEvent(new PlayerStateChangedEvent(result.ThiefId));

            return result;
        }
    }
}