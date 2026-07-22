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
}