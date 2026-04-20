using Catan.Core.DomainEvents;
using Catan.Core.Results;
using Catan.Core.Rules;
using Catan.Shared.Data;

namespace Catan.Core.PhaseLogic
{
    public sealed class ReactToTradeLogic : BaseLogic
    {
        public ReactToTradeLogic(GameSession session) : base(session) { }

        public ResultPlayerTrade Handle()
        {
            var (exists, context) = Session.TryGetPlayerTradeContext();

            if (!exists)
                return ResultPlayerTrade.Fail(ConditionFailureReason.DoesNotExist, default, default);

            var seller = Session.GetPlayerById(context.SellerId);
            var buyer = Session.GetPlayerById(context.BuyerId);

            var validation = RulesTrade.CanAcceptTrade(seller, buyer, context.Offered, context.Desired, context);

            if (!validation.Success)
                return ResultPlayerTrade.Fail(validation.Reason, context.SellerId, context.BuyerId);

            Session.PlayerTradeDoneMutation(seller, buyer, context.Offered, context.Desired);

            var result = ResultPlayerTrade.Ok(context.SellerId, context.BuyerId, context.Offered, context.Desired, EnumGamePhases.NormalRound);
            result.AddDomainEvent(new PlayerStateChangedEvent(result.SellerId));

            return result;
        }
    }
}