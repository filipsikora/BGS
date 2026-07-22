using Catan.Core.Conditions;
using Catan.Core.DomainEvents;
using Catan.Core.Results;
using Catan.Core.Rules;
using Catan.Core.Runtime;

namespace Catan.Core.UseCases
{
    public sealed class BuyDevCardLogic : BaseUseCase
    {
        public BuyDevCardLogic(GameSession session) : base(session) { }

        public ResultBuyDevCard Handle(int playerId)
        {
            var player = Session.GetPlayerById(playerId);
            var devCardsLeftList = Session.GetDevCardsLeft();

            var devCardsLeftValidation = ConditionsDevCards.DevCardsLeft(devCardsLeftList.Count);

            if (!devCardsLeftValidation.Success)
                return ResultBuyDevCard.Fail(devCardsLeftValidation.Reason, playerId);

            var devCard = Session.GetFirstDevCard();
            var devCardId = devCard.ID;
            var devCardType = devCard.Type;

            var validation = RulesDevCards.CanBuyDevCard(player, devCard, devCardsLeftList, Session);

            if (!validation.Success)
            {
                return ResultBuyDevCard.Fail(validation.Reason, player.ID);
            }

            Session.BuyDevCardMutation(devCard);

            var result = ResultBuyDevCard.Ok(player.ID, devCardId, devCardType, null);
            result.AddDomainEvent(new DevCardBoughtEvent(playerId, result.DevCardId.Value, result.Type.Value, player.DevelopmentCardsByID.Count, player.Resources.ToDictionary()));

            return ApplyPhase(result);
        }
    }
}
