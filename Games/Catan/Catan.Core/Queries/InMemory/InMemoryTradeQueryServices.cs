using Catan.Core.Engine;
using Catan.Core.Queries.Interfaces;
using Catan.Core.Rules;
using Catan.Core.Runtime;
using Catan.Core.Snapshots.ClientQueries;

namespace Catan.Core.Queries.InMemory
{
    public sealed class InMemoryTradeQueryServices : ITradeQueryService
    {
        private readonly GameSession _session;

        public InMemoryTradeQueryServices(GameSession session)
        {
            _session = session;
        }

        public TradeOfferedSnapshot GetTradeOfferData()
        {
            PlayerTradeContext? context = _session.TryGetPlayerTradeContext();

            var canTrade = RulesTrade.CanAcceptTrade(_session.GetPlayerById(context.SellerId), _session.GetPlayerById(context.BuyerId), context.Offered, context.Desired, context).Success;

            return new TradeOfferedSnapshot(context.SellerId, context.BuyerId, context.SellerName, context.BuyerName, context.Offered.ToDictionary(), context.Desired.ToDictionary(), canTrade);
        }
    }
}