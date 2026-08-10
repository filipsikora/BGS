using Catan.Shared.Data;

namespace Catan.Shared.Dtos.DomainEvents
{
    public sealed class CardsDiscardedEventPrivateDto(int playerId, Dictionary<EnumResourceType, int> resources, Dictionary<EnumResourceType, int> playerResources, Dictionary<EnumResourceType, int> bank)
    {
        public int PlayerId = playerId;
        public Dictionary<EnumResourceType, int> Resources = resources;
        public Dictionary<EnumResourceType, int> PlayerResources = playerResources;
        public Dictionary<EnumResourceType, int> Bank = bank;
    }

    public sealed class CardsDiscardedPublicEventDto(int playerId, Dictionary<EnumResourceType, int> resources, int resourcesCount, Dictionary<EnumResourceType, int> bank)
    {
        public int PlayerId = playerId;
        public Dictionary<EnumResourceType, int> Resources = resources;
        public int ResourcesCount = resourcesCount;
        public Dictionary<EnumResourceType, int> Bank = bank;
    }

    public sealed class CardStolenEventThiefDto(EnumResourceType resource, int thiefId, int victimId, Dictionary<EnumResourceType, int> thiefResources, int victimResourcesCount)
    {
        public EnumResourceType Resource = resource;
        public int ThiefId = thiefId;
        public int VictimId = victimId;
        public Dictionary<EnumResourceType, int> ThiefResources = thiefResources;
        public int VictimResourcesCount = victimResourcesCount;
    }

    public sealed class CardStolenEventVictimDto(EnumResourceType resource, int thiefId, int victimId, int thiefResourcesCount, Dictionary<EnumResourceType, int> victimResources)
    {
        public EnumResourceType Resource = resource;
        public int ThiefId = thiefId;
        public int VictimId = victimId;
        public int ThiefResourcesCount = thiefResourcesCount;
        public Dictionary<EnumResourceType, int> VictimResources = victimResources;
    }

    public sealed class CardStolenEventPublicDto(EnumResourceType resource, int thiefId, int victimId, int thiefResourcesCount, int victimResourcesCount)
    {
        public EnumResourceType Resource = resource;
        public int ThiefId = thiefId;
        public int VictimId = victimId;
        public int ThiefResourcesCount = thiefResourcesCount;
        public int VictimResourcesCount = victimResourcesCount;
    }
    public sealed class RobberPlacedEventDto(int hexId, bool canSteal)
    {
        public int HexId = hexId;
        public bool CanSteal = canSteal;
    }
}
