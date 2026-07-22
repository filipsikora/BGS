using Catan.Application.Controllers;
using Catan.Application.UIMessages;
using Catan.Application.Commands;

namespace Catan.Application.Phases
{
    public class MonopolyCardPhase : BasePhase
    {
        public MonopolyCardPhase(Facade facade) : base(facade) { }

        public override GameResult Handle(object command, int playerId)
        {
            switch (command)
            {
                case StolenCardSelectedCommand c:
                    return HandleResourceAccepted(c, playerId);

                default:
                    return GameResult.Fail();
            }
        }

        private GameResult HandleResourceAccepted(StolenCardSelectedCommand signal, int playerId)
        {
            var result = Facade.UseMonopolyCard(signal.Type, playerId);

            if (!result.Success)
            {
                return GameResult.Fail().AddUIMessage(new ActionRejectedMessage(result.ThiefId, result.Reason));
            }

            return GameResult.Ok(result.NextPhase).AddDomainEventsList(result.DomainEvents);
        }
    }
}