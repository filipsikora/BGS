using Catan.Core.DomainEvents;
using Catan.Core.Results;
using Catan.Core.Rules;

namespace Catan.Core.PhaseLogic
{
    public sealed class BuyDevCardLogic : BaseLogic
    {
        public BuyDevCardLogic(GameSession session) : base(session) { }

        public ResultBuyDevCard Handle()
        {
            var player = Session.GetCurrentPlayer();
            var devCard = Session.GetFirstDevCard();
            var devCardId = devCard.ID;
            var devCardType = devCard.Type;
            var devCardsLeftList = Session.GetDevCardsLeft();

            var validation = RulesDevCards.CanBuyDevCard(player, devCard, devCardsLeftList);

            if (!validation.Success)
            {
                return ResultBuyDevCard.Fail(validation.Reason, player.ID);
            }

            Session.BuyDevCardMutation(devCard);

            var result = ResultBuyDevCard.Ok(player.ID, devCardId, devCardType, null);
            result.AddDomainEvent(new DevelopmentCardBoughtEvent(result.DevCardId.Value)).AddDomainEvent(new PlayerStateChangedEvent(result.PlayerId));

            return result;
        }
    }
}
