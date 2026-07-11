using Catan.Core.Interfaces;
using Catan.Shared.Data;

namespace Catan.Core.DomainEvents
{
    public sealed class BankTradeDoneEvent : IDomainEvent
    {
        public EnumDomainEvents Type => EnumDomainEvents.BankTradeDoneEvent;
        public int PlayerId { get; set; }
        public EnumResourceType Offered { get; set; }
        public EnumResourceType Desired { get; set; }
        public int Ratio { get; set; }

        public BankTradeDoneEvent(int playerId, EnumResourceType offered, EnumResourceType desired, int ratio)
        {
            PlayerId = playerId;
            Offered = offered;
            Desired = desired;
            Ratio = ratio;
        }
    }
}