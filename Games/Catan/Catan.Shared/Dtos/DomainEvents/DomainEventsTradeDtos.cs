using Catan.Shared.Data;

namespace Catan.Shared.Dtos.DomainEvents
{
    public sealed class BankTradeDoneEventDto(int playerId, EnumResourceType offered, EnumResourceType desired, int ratio, Dictionary<EnumResourceType, int> bank, 
        Dictionary<EnumResourceType, int> playerResources)
    {
        public int PlayerId = playerId;
        public EnumResourceType Offered = offered;
        public EnumResourceType Desired = desired;
        public int Ratio = ratio;
        public Dictionary<EnumResourceType, int> Bank = bank;
        public Dictionary<EnumResourceType, int> PlayerResources = playerResources;
    }

    public sealed class TradeDoneEventSellerDto(int sellerId, int buyerId, Dictionary<EnumResourceType, int>  sellerResources, int buyerResourcesCount, Dictionary<EnumResourceType, int> offered,
        Dictionary<EnumResourceType, int> desired)
    {
        public int SellerId = sellerId;
        public int BuyerId = buyerId;
        public Dictionary<EnumResourceType, int> SellerResources = sellerResources;
        public int BuyerResourcesCount = buyerResourcesCount;
        public Dictionary<EnumResourceType, int> Offered = offered;
        public Dictionary<EnumResourceType, int> Desired = desired;
    }

    public sealed class TradeDoneEventBuyerDto(int sellerId, int buyerId, int sellerResourcesCount, Dictionary<EnumResourceType, int> buyerResources, Dictionary<EnumResourceType, int> offered,
        Dictionary<EnumResourceType, int> desired)
    {
        public int SellerId = sellerId;
        public int BuyerId = buyerId;
        public int SellerResourcesCount = sellerResourcesCount;
        public Dictionary<EnumResourceType, int> BuyerResources = buyerResources;
        public Dictionary<EnumResourceType, int> Offered = offered;
        public Dictionary<EnumResourceType, int> Desired = desired;
    }

    public sealed class TradeDoneEventPublicDto(int sellerId, int buyerId, int sellerResourcesCount, int buyerResourcesCount, Dictionary<EnumResourceType, int> offered, 
        Dictionary<EnumResourceType, int> desired)
    {
        public int SellerId = sellerId;
        public int BuyerId = buyerId;
        public int SellerResourcesCount = sellerResourcesCount;
        public int BuyerResourcesCount = buyerResourcesCount;
        public Dictionary<EnumResourceType, int> Offered = offered;
        public Dictionary<EnumResourceType, int> Desired = desired;
    }
}