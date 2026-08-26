using Catan.Shared.Data;
using Catan.Shared.Interfaces;

namespace Catan.Shared.Dtos.DomainEvents
{
    public sealed class DevCardUsedEventPrivateDto(int playerId, int cardId, EnumDevelopmentCardTypes cardType, int devCardNumber, List<DevelopmentCardDto> devCards) : IDomainEventDto
    {
        public int PlayerId = playerId;
        public int CardId = cardId;
        public EnumDevelopmentCardTypes CardType = cardType;
        public int DevCardNumber = devCardNumber;
        public List<DevelopmentCardDto> DevCards = devCards;
    }

    public sealed class DevCardUsedEventPublicDto(int playerId, int devCardNumber) : IDomainEventDto
    {
        public int PlayerId = playerId;
        public int DevCardNumber = devCardNumber;
    }

    public sealed class CardsStolenEventThiefDto(EnumResourceType resource, int quantity, int thiefId, int victimId, Dictionary<EnumResourceType, int> thiefResources, int victimResourcesCount,
        int thiefResourcesCount) : IDomainEventDto
    {
        public EnumResourceType Resource = resource;
        public int Quantity = quantity;
        public int ThiefId = thiefId;
        public int VictimId = victimId;
        public Dictionary<EnumResourceType, int> ThiefResources = thiefResources;
        public int VictimResourcesCount = victimResourcesCount;
        public int ThiefResourcesCount = thiefResourcesCount;
    }

    public sealed class CardsStolenEventVictimDto(EnumResourceType resource, int quantity, int thiefId, int victimId, int thiefResourcesCount, Dictionary<EnumResourceType, int> victimResources,
        int victimResourcesCount) : IDomainEventDto
    {
        public EnumResourceType Resource = resource;
        public int Quantity = quantity;
        public int ThiefId = thiefId;
        public int VictimId = victimId;
        public int ThiefResourcesCount = thiefResourcesCount;
        public Dictionary<EnumResourceType, int> VictimResources = victimResources;
        public int VictimResourcesCount = victimResourcesCount;
    }

    public sealed class CardsStolenEventPublicDto(EnumResourceType resource, int quantity, int thiefId, int victimId, int thiefResourcesCount, int victimResourcesCount) : IDomainEventDto
    {
        public EnumResourceType Resource = resource;
        public int Quantity = quantity;
        public int ThiefId = thiefId;
        public int VictimId = victimId;
        public int ThiefResourcesCount = thiefResourcesCount;
        public int VictimResourcesCount = victimResourcesCount;
    }

    public sealed class DevCardBoughtEventPrivateDto(int playerId, int cardId, EnumDevelopmentCardTypes devCardType, int devCardNumber, Dictionary<EnumResourceType, int> resources, bool isPlayable,
        int resourceCardsCount) : IDomainEventDto
    {
        public int PlayerId = playerId;
        public int CardId = cardId;
        public EnumDevelopmentCardTypes DevCardType = devCardType;
        public int DevCardNumber = devCardNumber;
        public Dictionary<EnumResourceType, int> Resources = resources;
        public bool IsPlayable = isPlayable;
        public int ResourceCardsCount = resourceCardsCount;
    }

    public sealed class DevCardBoughtEventPublicDto(int playerId, int devCardNumber, int resourcesNumber) : IDomainEventDto
    {
        public int PlayerId = playerId;
        public int DevCardNumber = devCardNumber;
        public int ResourcesNumber = resourcesNumber;
    }

    public sealed class VictoryCardUsedEventDto(int playerId, int extraPoints, int victoryCardsUsed) : IDomainEventDto
    {
        public int PlayerId = playerId;
        public int ExtraPoints = extraPoints;
        public int VictoryCardsUsed = victoryCardsUsed;
    }

    public sealed class KnightCardUsedEventDto(int playerId, int knightCardsUsed) : IDomainEventDto
    {
        public int PlayerId = playerId;
        public int KnightCardsUsed = knightCardsUsed;
    }

    public sealed class DevCardPlayabilityChangedEventPrivateDto(IEnumerable<int> devCardsPlayable)
    {
        public IEnumerable<int> DevCardsPlayable = devCardsPlayable;
    }

    public sealed class DevCardPlayabilityChangedEventPublicDto() { }
}
