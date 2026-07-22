using Catan.Application.Controllers;
using Catan.Application.Commands;
using Catan.Application.UIMessages;

namespace Catan.Application.Phases
{
    public class CardStealingPhase : BasePhase
    {
        public CardStealingPhase(Facade facade) : base(facade) { }

        public override GameResult Handle(object command, int playerId) 
        {
            switch (command)
            {
                case StolenCardSelectedCommand c:
                    return HandleSteal(c, playerId);

                default:
                    return GameResult.Fail();
            }
        }

        private GameResult HandleSteal(StolenCardSelectedCommand signal, int playerId)
        {
            var victimId = Facade.GetVictimId();
            var result = Facade.UseSteal(victimId, signal.Type, playerId);

            if (!result.Success) 
            {
                return GameResult.Fail().AddUIMessage(new ActionRejectedMessage(result.ThiefId, result.Reason));
            }

            return GameResult.Ok(result.NextPhase).AddDomainEventsList(result.DomainEvents);
        }
    }
}