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

    public sealed class DevCardUsedEventPublicDto(int playerId, int devCardNumber, EnumDevelopmentCardTypes cardTypes) : IDomainEventDto
    {
        public int PlayerId = playerId;
        public int DevCardNumber = devCardNumber;
        public EnumDevelopmentCardTypes CardTypes = cardTypes;
    }

    public sealed class CardsStolenEventDto(EnumResourceType resource, int thiefId, Dictionary<int, int> victimIdsToAmounts, Dictionary<EnumResourceType, int> myResources, int myResourcesCount,
        Dictionary<int, int> playersResourcesCount) : IDomainEventDto
    {
        public EnumResourceType Resource = resource;
        public int ThiefId = thiefId;
        public Dictionary<int, int> VictimIdsToAmounts = victimIdsToAmounts;
        public Dictionary<EnumResourceType, int> MyResources = myResources;
        public int MyResourcesCount = myResourcesCount;
        public Dictionary<int, int> PlayersResourcesCount = playersResourcesCount;
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
}
