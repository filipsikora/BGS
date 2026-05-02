using Catan.Core.Models;
using Catan.Core.Results;
using Catan.Core.Rules;
using Catan.Shared.Data;

namespace Catan.Core.UseCases
{
    public sealed class PrepareTradeOfferLogic : BaseUseCase
    {
        public PrepareTradeOfferLogic(GameSession session) : base(session) { }

        public ResultCondition Handle(ResourceCostOrStock offered)
        {
            var player = Session.GetCurrentPlayer();
            var result = RulesTrade.CanDraftTrade(player, offered);

            if (!result.Success)
            {
                return ResultCondition.Fail(result.Reason);
            }

            Session.CreateTradeDraftContext(offered);

            return ApplyPhase(ResultCondition.Ok(EnumGamePhases.TradeOffer));
        }
    }
}
