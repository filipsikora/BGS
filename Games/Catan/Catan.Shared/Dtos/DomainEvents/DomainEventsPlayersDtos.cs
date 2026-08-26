using Catan.Shared.Data;
using Catan.Shared.Interfaces;

namespace Catan.Shared.Dtos.DomainEvents
{
    public sealed class PlayerResourcesReceivedEventPrivateDto(int playerId, Dictionary<EnumResourceType, int> resourcesChange, Dictionary<EnumResourceType, int> playerResources,
        Dictionary<EnumResourceType, int> bank, int playerResourcesCount) : IDomainEventDto
    {
        public int PlayerId = playerId;
        public Dictionary<EnumResourceType, int> ResourcesChange = resourcesChange;
        public Dictionary<EnumResourceType, int> PlayerResources = playerResources;
        public Dictionary<EnumResourceType, int> Bank = bank;
        public int PlayerResourcesCount = playerResourcesCount;
    }

    public sealed class PlayerResourcesReceivedEventPublicDto(int playerId, Dictionary<EnumResourceType, int> resourcesChange, int playerResourcesCount, Dictionary<EnumResourceType, int> bank) : IDomainEventDto
    {
        public int PlayerId = playerId;
        public Dictionary<EnumResourceType, int> ResourcesChange = resourcesChange;
        public int PlayerResourcesCount = playerResourcesCount;
        public Dictionary<EnumResourceType, int> Bank = bank;
    }

    public sealed class RoadChampionChangedEventDto(int? oldChampionId, int? newChampionId, int? oldChampionExtraPoints, int? newChampionExtraPoints, int? oldChampionPoints, int? newChampionPoints) : IDomainEventDto
    {
        public int? OldChampionId = oldChampionId;
        public int? NewChampionId = newChampionId;
        public int? OldChampionExtraPoints = oldChampionExtraPoints;
        public int? NewChampionExtraPoints = newChampionExtraPoints;
        public int? OldChampionPoints = oldChampionPoints;
        public int? NewChampionPoints = newChampionPoints;
    }

    public sealed class KnightChampionChangedEventDto(int? oldChampionId, int? newChampionId, int? oldChampionExtraPoints, int? newChampionExtraPoints, int? oldChampionPoints, int? newChampionPoints) : IDomainEventDto
    {
        public int? OldChampionId = oldChampionId;
        public int? NewChampionId = newChampionId;
        public int? OldChampionExtraPoints = oldChampionExtraPoints;
        public int? NewChampionExtraPoints = newChampionExtraPoints;
        public int? OldChampionPoints = oldChampionPoints;
        public int? NewChampionPoints = newChampionPoints;
    }
}