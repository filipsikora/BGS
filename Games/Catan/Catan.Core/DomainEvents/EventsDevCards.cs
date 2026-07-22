using Catan.Core.Interfaces;
using Catan.Shared.Data;

namespace Catan.Core.DomainEvents
{
    public sealed class DevCardBoughtEvent(int playerId, int cardId, EnumDevelopmentCardTypes devCardType, int devCardNumber, Dictionary<EnumResourceType, int> resources) : IDomainEvent
    {
        public EnumDomainEvents Type => EnumDomainEvents.DevCardBoughtEvent;

        public int PlayerId = playerId;
        public int CardId = cardId;
        public EnumDevelopmentCardTypes DevCardType = devCardType;
        public int DevCardNumber = devCardNumber;
        public Dictionary<EnumResourceType, int> Resources = resources;
    }

    public sealed class DevCardUsedEvent(int playerId, int cardId, EnumDevelopmentCardTypes cardType, int devCardNumber) : IDomainEvent
    {
        public EnumDomainEvents Type => EnumDomainEvents.DevCardPlayedEvent;
        public int PlayerId = playerId;
        public int CardId = cardId;
        public EnumDevelopmentCardTypes CardType = cardType;
        public int DevCardNumber = devCardNumber;
    }

    public sealed class CardsStolenEvent(EnumResourceType resource, int quantity, int thiefId, int victimId, Dictionary<EnumResourceType, int> thiefResources,
    Dictionary<EnumResourceType, int> victimResources) : IDomainEvent
    {
        public EnumDomainEvents Type => EnumDomainEvents.CardsStolenEvent;

        public EnumResourceType Resource = resource;
        public int Quantity = quantity;
        public int ThiefId = thiefId;
        public int VictimId = victimId;
        public Dictionary<EnumResourceType, int> ThiefResources = thiefResources;
        public Dictionary<EnumResourceType, int> VictimResources = victimResources;
    }

    public sealed class VictoryCardUsedEvent(int playerId, int extraPoints, int victoryCardsUsed) : IDomainEvent
    {
        public EnumDomainEvents Type => EnumDomainEvents.VictoryCardUsedEvent;

        public int PlayerId = playerId;
        public int ExtraPoints = extraPoints;
        public int VictoryCardsUsed = victoryCardsUsed;
    }

    public sealed class KnightCardUsedEvent(int playerId, int knightCardsUsed) : IDomainEvent
    {
        public EnumDomainEvents Type => EnumDomainEvents.KnightCardUsedEvent;

        public int PlayerId = playerId;
        public int KnightCardsUsed = knightCardsUsed;
    }
}