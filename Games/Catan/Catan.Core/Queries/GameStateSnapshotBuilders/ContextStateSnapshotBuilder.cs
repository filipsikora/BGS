using Catan.Core.Engine;
using Catan.Core.Runtime;
using Catan.Core.Snapshots.ClientQueries;

namespace Catan.Core.Queries.GameStateSnapshotBuilders
{
    public class ContextStateSnapshotBuilder
    {
        private readonly GameSession _session;

        public ContextStateSnapshotBuilder(GameSession session)
        {
            _session = session;
        }

        public FullPhaseContextSnapshot GetFullPhaseContextData()
        {
            return new FullPhaseContextSnapshot(GetTradeOfferContextData(), GetTradeRequestContextData(), GetRoadBuildingContextSnapshot(), GetCardDiscardingContextSnapshot(), GetCardStealingContextSnapshot());
        }

        private TradeOfferContextSnapshot? GetTradeOfferContextData()
        {
            PlayerTradeContext? context = _session.TryGetPlayerTradeContext();

            if (context == null)
                return null;

            return new TradeOfferContextSnapshot(
                context.SellerId,
                context.BuyerId,
                context.SellerName,
                context.BuyerName,
                context.Offered.ToDictionary(),
                context.Desired.ToDictionary()
                );
        }

        private TradeRequestContextSnapshot? GetTradeRequestContextData()
        {
            TradeDraftContext? context = _session.TryGetTradeDraftContext();

            if (context == null)
                return null;

            return new TradeRequestContextSnapshot(
                context.Offered.ToDictionary()
                );
        }

        private RoadBuildingContextSnapshot? GetRoadBuildingContextSnapshot()
        {
            RoadBuildingContext? context = _session.GetRoadBuildingContext();

            if (context == null)
                return null;

            return new RoadBuildingContextSnapshot(
                context.RoadsLeftToBuild
                );
        }

        private CardDiscardContextSnapshot? GetCardDiscardingContextSnapshot()
        {
            CardDiscardContext? context = _session.GetCardDiscardingContext();

            if (context == null)
                return null;

            return new CardDiscardContextSnapshot(
                context.PlayersToDiscard
                );
        }

        private CardStealingContextSnapshot? GetCardStealingContextSnapshot()
        {
            CardStealingContext? context = _session.GetCardStealingContext();

            if (context == null)
                return null;

            return new CardStealingContextSnapshot(
                context.VictimId
                );
        }
    }
}