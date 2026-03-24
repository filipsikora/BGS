using Catan.Shared.Data;
using System.Collections.Generic;

namespace Catan.Core.Snapshots
{
    public sealed class TradeOfferedSnapshot
    {
        public int SellerId { get; }
        public int BuyerId { get; }
        public string SellerName { get; }
        public string BuyerName { get; }
        public Dictionary<EnumResourceType, int> Offered { get; }
        public Dictionary<EnumResourceType, int> Desired { get; }
        public bool CanTrade { get; }

        public TradeOfferedSnapshot(int sellerId, int buyerId, string sellerName, string buyerName, Dictionary<EnumResourceType, int> offered, Dictionary<EnumResourceType, int> desired, bool canTrade)
        {
            SellerId = sellerId;
            BuyerId = buyerId;
            SellerName = sellerName;
            BuyerName = buyerName;
            Offered = offered;
            Desired = desired;
            CanTrade = canTrade;
        }
    }
}
