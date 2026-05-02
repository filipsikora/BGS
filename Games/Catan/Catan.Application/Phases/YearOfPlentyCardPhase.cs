using Catan.Application.Controllers;
using Catan.Application.UIMessages;
using Catan.Core.Models;
using Catan.Application.Commands;
using Catan.Shared.Data;

namespace Catan.Application.Phases
{
    public class YearOfPlentyCardPhase : BasePhase
    {
        private ResourceCostOrStock _cardsDesired = new();
        private readonly int _cardsToReceive = 2;

        public YearOfPlentyCardPhase(Facade facade) : base(facade) { }

        public override GameResult Handle(object command)
        {
            switch (command)
            {
                case ResourceCardSelectedCommand c:
                    return HandleResourceCardClicked(c);

                case CardSelectionAcceptedCommand c:
                    return HandleResourcesSelected(c);

                default:
                    return GameResult.Fail();
            }
        }

        private GameResult HandleResourceCardClicked(ResourceCardSelectedCommand signal)
        {
            EnumResourceType type = signal.Type;

            if (!signal.IsSelected)
            {
                if (_cardsDesired.Get(type) > 0)
                {
                    _cardsDesired.SubtractExactAmount(type, 1);
                }
            }

            if (signal.IsSelected)
            {
                _cardsDesired.AddExactAmount(type, 1);

            }

            bool canAccept = Facade.CheckIfExactCardsAmountSelected(_cardsDesired, _cardsToReceive);

            return GameResult.Ok().AddUIMessage(new SelectionChangedMessage(canAccept));
        }

        private GameResult HandleResourcesSelected(CardSelectionAcceptedCommand signal)
        {
            var playerId = Facade.GetCurrentPlayerId();
            var validation = Facade.UseYearOfPlenty(_cardsDesired);

            if (!validation.Success)
            {
                return GameResult.Fail().AddUIMessage(new ActionRejectedMessage(playerId, validation.Reason));
            }
            var result = GameResult.Ok();

            foreach (var (key, amount) in validation.Requested.ResourceDictionary)
            {
                if (amount <= 0)
                    continue;

                result.AddUIMessage(new LogMessageMessage(EnumLogTypes.Info, $"Player{playerId} received {key} {amount} from Year Of Plenty card"));
            }

            return GameResult.Ok(validation.NextPhase).AddDomainEventsList(validation.DomainEvents);
        }
    }
}