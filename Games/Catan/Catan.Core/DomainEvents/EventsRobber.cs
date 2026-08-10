using Catan.Core.Interfaces;
using Catan.Shared.Data;

namespace Catan.Core.DomainEvents
{
    public sealed class CardsDiscardedEvent(int playerId, Dictionary<EnumResourceType, int> resources, Dictionary<EnumResourceType, int> playerResources, Dictionary<EnumResourceType, int> bank) : IDomainEvent
    {
        public EnumDomainEvents Type => EnumDomainEvents.CardsDiscardedEvent;

        public int PlayerId = playerId;
        public Dictionary<EnumResourceType, int> Resources = resources;
        public Dictionary<EnumResourceType, int> PlayerResources = playerResources;
        public Dictionary<EnumResourceType, int> Bank = bank;
    }

    public sealed class CardStolenEvent(EnumResourceType resource, int thiefId, int victimId, Dictionary<EnumResourceType, int> thiefResources, Dictionary<EnumResourceType, 
        int> victimResources) : IDomainEvent
    {
        public EnumDomainEvents Type => EnumDomainEvents.CardStolenEvent;

        public EnumResourceType Resource = resource;
        public int ThiefId = thiefId;
        public int VictimId = victimId;
        public Dictionary<EnumResourceType, int> ThiefResources = thiefResources;
        public Dictionary<EnumResourceType, int> VictimResources = victimResources;
    }

    public sealed class RobberPlacedEvent(int hexId, bool canSteal) : IDomainEvent
    {
        public EnumDomainEvents Type => EnumDomainEvents.RobberPlacedEvent;

        public int HexId = hexId;
        public bool CanSteal = canSteal;
    }
}