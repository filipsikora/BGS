using Catan.Shared.Data;

namespace Catan.Core.Snapshots.ClientQueries
{
    public sealed class FullPhaseContextSnapshot
    {
        public TradeOfferContextSnapshot? TradeOffer;
        public TradeRequestContextSnapshot? TradeDraft;
        public RoadBuildingContextSnapshot? RoadBuilding;
        public CardDiscardContextSnapshot? CardDiscarding;
        public CardStealingContextSnapshot? CardStealing;

        public FullPhaseContextSnapshot(TradeOfferContextSnapshot? tradeOffer, TradeRequestContextSnapshot? tradeDraft, RoadBuildingContextSnapshot? roadBuilding, CardDiscardContextSnapshot? cardDiscarding, CardStealingContextSnapshot? cardStealing)
        {
            TradeOffer = tradeOffer;
            TradeDraft = tradeDraft;
            RoadBuilding = roadBuilding;
            CardDiscarding = cardDiscarding;
            CardStealing = cardStealing;
        }
    }
    public sealed class TradeOfferContextSnapshot
    {
        public int SellerId { get; }
        public int BuyerId { get; }
        public string SellerName { get; }
        public string BuyerName { get; }
        public Dictionary<EnumResourceType, int> Offered { get; }
        public Dictionary<EnumResourceType, int> Desired { get; }

        public TradeOfferContextSnapshot(int sellerId, int buyerId, string sellerName, string buyerName, Dictionary<EnumResourceType, int> offered, Dictionary<EnumResourceType, int> desired)
        {
            SellerId = sellerId;
            BuyerId = buyerId;
            SellerName = sellerName;
            BuyerName = buyerName;
            Offered = offered;
            Desired = desired;
        }
    }

    public sealed class TradeRequestContextSnapshot
    {
        public Dictionary<EnumResourceType, int> Offered { get; }

        public TradeRequestContextSnapshot(Dictionary<EnumResourceType, int> offered)
        {
            Offered = offered;
        }
    }

    public sealed class RoadBuildingContextSnapshot
    {
        public int RoadsLeftToBuild { get; set; }

        public RoadBuildingContextSnapshot(int roadsLeftToBuild)
        {
            RoadsLeftToBuild = roadsLeftToBuild;
        }
    }

    public sealed class CardDiscardContextSnapshot
    {
        public Queue<int> PlayersToDiscard { get; }

        public CardDiscardContextSnapshot(IEnumerable<int> playerIds)
        {
            PlayersToDiscard = new Queue<int>(playerIds);
        }
    }

    public sealed class CardStealingContextSnapshot
    {
        public int VictimId { get; }

        public CardStealingContextSnapshot(int victimId)
        {
            VictimId = victimId;
        }
    }
}
