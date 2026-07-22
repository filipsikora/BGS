using Catan.Shared.Data;

namespace Catan.Shared.Dtos.DomainEvents
{
    public sealed class DevCardUsedEventDto(int playerId, int cardId, EnumDevelopmentCardTypes cardType, int devCardNumber)
    {
        public int PlayerId = playerId;
        public int CardId = cardId;
        public EnumDevelopmentCardTypes CardType = cardType;
        public int DevCardNumber = devCardNumber;
    }

    public sealed class CardsStolenEventThiefDto(EnumResourceType resource, int quantity, int thiefId, int victimId, Dictionary<EnumResourceType, int> thiefResources, int victimResourcesCount)
    {
        public EnumResourceType Resource = resource;
        public int Quantity = quantity;
        public int ThiefId = thiefId;
        public int VictimId = victimId;
        public Dictionary<EnumResourceType, int> ThiefResources = thiefResources;
        public int VictimResourcesCount = victimResourcesCount;
    }

    public sealed class CardsStolenEventVictimDto(EnumResourceType resource, int quantity, int thiefId, int victimId, int thiefResourcesCount, Dictionary<EnumResourceType, int> victimResources)
    {
        public EnumResourceType Resource = resource;
        public int Quantity = quantity;
        public int ThiefId = thiefId;
        public int VictimId = victimId;
        public int ThiefResourcesCount = thiefResourcesCount;
        public Dictionary<EnumResourceType, int> VictimResources = victimResources;
    }

    public sealed class CardsStolenEventPublicDto(EnumResourceType resource, int quantity, int thiefId, int victimId, int thiefResourcesCount, int victimResourcesCount)
    {
        public EnumResourceType Resource = resource;
        public int Quantity = quantity;
        public int ThiefId = thiefId;
        public int VictimId = victimId;
        public int ThiefResourcesCount = thiefResourcesCount;
        public int VictimResourcesCount = victimResourcesCount;
    }

    public sealed class DevCardBoughtEventPrivateDto(int playerId, int cardId, EnumDevelopmentCardTypes devCardType, int devCardNumber, Dictionary<EnumResourceType, int> resources)
    {
        public int PlayerId = playerId;
        public int CardId = cardId;
        public EnumDevelopmentCardTypes DevCardType = devCardType;
        public int DevCardNumber = devCardNumber;
        public Dictionary<EnumResourceType, int> Resources = resources;
    }

    public sealed class DevCardBoughtEventPublicDto(int playerId, int devCardNumber, int resourcesNumber)
    {
        public int PlayerId = playerId;
        public int DevCardNumber = devCardNumber;
        public int ResourcesNumber = resourcesNumber;
    }

    public sealed class VictoryCardUsedEventDto(int playerId, int extraPoints, int victoryCardsUsed)
    {
        public int PlayerId = playerId;
        public int ExtraPoints = extraPoints;
        public int VictoryCardsUsed = victoryCardsUsed;
    }

    public sealed class KnightCardUsedEventDto(int playerId, int knightCardsUsed)
    {
        public int PlayerId = playerId;
        public int KnightCardsUsed = knightCardsUsed;
    }
}
