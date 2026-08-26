using Catan.Shared.Data;
using Catan.Shared.Interfaces;

namespace Catan.Shared.Dtos.DomainEvents
{
    public sealed class BankTradeDoneEventPrivateDto(int playerId, EnumResourceType offered, EnumResourceType desired, int ratio, Dictionary<EnumResourceType, int> bank, 
        Dictionary<EnumResourceType, int> playerResources, int playerResourcesCount) : IDomainEventDto
    {
        public int PlayerId = playerId;
        public EnumResourceType Offered = offered;
        public EnumResourceType Desired = desired;
        public int Ratio = ratio;
        public Dictionary<EnumResourceType, int> Bank = bank;
        public Dictionary<EnumResourceType, int> PlayerResources = playerResources;
        public int PlayerResourcesCount = playerResourcesCount;
    }

    public sealed class BankTradeDoneEventPublicDto(int playerId, EnumResourceType offered, EnumResourceType desired, int ratio, Dictionary<EnumResourceType, int> bank,
    int playerResourcesCount) : IDomainEventDto
    {
        public int PlayerId = playerId;
        public EnumResourceType Offered = offered;
        public EnumResourceType Desired = desired;
        public int Ratio = ratio;
        public Dictionary<EnumResourceType, int> Bank = bank;
        public int PlayerResourcesCount = playerResourcesCount;
    }

    public sealed class TradeDoneEventSellerDto(int sellerId, int buyerId, Dictionary<EnumResourceType, int>  sellerResources, int buyerResourcesCount, Dictionary<EnumResourceType, int> offered,
        Dictionary<EnumResourceType, int> desired, int sellerResourcesCount) : IDomainEventDto
    {
        public int SellerId = sellerId;
        public int BuyerId = buyerId;
        public Dictionary<EnumResourceType, int> SellerResources = sellerResources;
        public int BuyerResourcesCount = buyerResourcesCount;
        public int SellerResourcesCount = sellerResourcesCount;
        public Dictionary<EnumResourceType, int> Offered = offered;
        public Dictionary<EnumResourceType, int> Desired = desired;
    }

    public sealed class TradeDoneEventBuyerDto(int sellerId, int buyerId, int sellerResourcesCount, Dictionary<EnumResourceType, int> buyerResources, Dictionary<EnumResourceType, int> offered,
        Dictionary<EnumResourceType, int> desired, int buyerResourcesCount) : IDomainEventDto
    {
        public int SellerId = sellerId;
        public int BuyerId = buyerId;
        public int SellerResourcesCount = sellerResourcesCount;
        public Dictionary<EnumResourceType, int> BuyerResources = buyerResources;
        public int BuyerResourcesCount = buyerResourcesCount;
        public Dictionary<EnumResourceType, int> Offered = offered;
        public Dictionary<EnumResourceType, int> Desired = desired;
    }

    public sealed class TradeDoneEventPublicDto(int sellerId, int buyerId, int sellerResourcesCount, int buyerResourcesCount, Dictionary<EnumResourceType, int> offered, 
        Dictionary<EnumResourceType, int> desired) : IDomainEventDto
    {
        public int SellerId = sellerId;
        public int BuyerId = buyerId;
        public int SellerResourcesCount = sellerResourcesCount;
        public int BuyerResourcesCount = buyerResourcesCount;
        public Dictionary<EnumResourceType, int> Offered = offered;
        public Dictionary<EnumResourceType, int> Desired = desired;
    }
}