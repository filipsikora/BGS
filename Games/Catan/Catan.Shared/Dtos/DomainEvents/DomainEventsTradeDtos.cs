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
}