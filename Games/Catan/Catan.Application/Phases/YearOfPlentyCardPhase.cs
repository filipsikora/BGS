using Catan.Application.Controllers;
using Catan.Application.UIMessages;
using Catan.Application.Commands;

namespace Catan.Application.Phases
{
    public class YearOfPlentyCardPhase : BasePhase
    {

        public YearOfPlentyCardPhase(Facade facade) : base(facade) { }

        public override GameResult Handle(object command, int playerId)
        {
            switch (command)
            {
                case CardsSelectedCommand c:
                    return HandleResourcesSelected(c, playerId);

                default:
                    return GameResult.Fail();
            }
        }

        private GameResult HandleResourcesSelected(CardsSelectedCommand signal, int playerId)
        {
            var validation = Facade.UseYearOfPlenty(signal.Resources, playerId);

            if (!validation.Success)
            {
                return GameResult.Fail().AddUIMessage(new ActionRejectedMessage(playerId, validation.Reason));
            }

            return GameResult.Ok(validation.NextPhase).AddDomainEventsList(validation.DomainEvents);
        }
    }
}