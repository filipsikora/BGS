using Catan.Application.Controllers;
using Catan.Application.Commands;
using Catan.Application.UIMessages;
using Catan.Core.DomainEvents;

namespace Catan.Application.Phases
{
    public class CardStealingPhase : BasePhase
    {
        public CardStealingPhase(Facade facade) : base(facade) { }

        public override GameResult Handle(object command) 
        {
            switch (command)
            {
                case StolenCardSelectedCommand c:
                    return HandleSteal(c);

                default:
                    return GameResult.Fail();
            }
        }

        private GameResult HandleSteal(StolenCardSelectedCommand signal)
        {
            var victimId = Facade.GetVictimId();
            var result = Facade.UseSteal(victimId, signal.Type);

            if (!result.Success) 
            {
                return GameResult.Fail().AddUIMessage(new ActionRejectedMessage(result.ThiefId, result.Reason));
            }

            return GameResult.Ok(result.NextPhase).AddDomainEvent(new PlayerStateChangedEvent(result.ThiefId));
        }
    }
}