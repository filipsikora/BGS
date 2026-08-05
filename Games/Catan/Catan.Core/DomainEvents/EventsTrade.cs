using Catan.Core.Interfaces;
using Catan.Shared.Data;

namespace Catan.Core.DomainEvents
{
    public sealed class BankTradeDoneEvent(int playerId, EnumResourceType offered, EnumResourceType desired, int ratio, Dictionary<EnumResourceType, int> bank, 
        Dictionary<EnumResourceType, int> playerResources) : IDomainEvent
    {
        public EnumDomainEvents Type => EnumDomainEvents.BankTradeDoneEvent;

        public int PlayerId = playerId;
        public EnumResourceType Offered = offered;
        public EnumResourceType Desired = desired;
        public int Ratio = ratio;
        public Dictionary<EnumResourceType, int> Bank = bank;
        public Dictionary<EnumResourceType, int> PlayerResources = playerResources;
    }

    public sealed class TradeDoneEvent(int sellerId, int buyerId, Dictionary<EnumResourceType, int> sellerResources, Dictionary<EnumResourceType, int> buyerResources, 
        Dictionary<EnumResourceType, int> offered, Dictionary<EnumResourceType, int> desired) : IDomainEvent
    {
        public EnumDomainEvents Type => EnumDomainEvents.TradeDoneEvent;

        public int SellerId = sellerId;
        public int BuyerId = buyerId;
        public Dictionary<EnumResourceType, int> SellerResources = sellerResources;
        public Dictionary<EnumResourceType, int> BuyerResources = buyerResources;
        public Dictionary<EnumResourceType, int> Offered = offered;
        public Dictionary<EnumResourceType, int> Desired = desired;
    }
}