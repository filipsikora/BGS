using Catan.Core.Interfaces;
using Catan.Shared.Data;
using Catan.Shared.Interfaces;

namespace Catan.Core.DomainEvents
{
    public sealed class PlayerResourcesReceivedEvent(int playerId, Dictionary<EnumResourceType, int> resourcesChange, Dictionary<EnumResourceType, int> playerResources,
        Dictionary<EnumResourceType, int> bank) : IDomainEvent
    {
        public EnumDomainEvents Type => EnumDomainEvents.PlayerResourcesReceivedEvent;

        public int PlayerId = playerId;
        public Dictionary<EnumResourceType, int> ResourcesChange = resourcesChange;
        public Dictionary<EnumResourceType, int> PlayerResources = playerResources;
        public Dictionary<EnumResourceType, int> Bank = bank;
    }

    public sealed class RoadChampionChangedEvent(int? oldChampionId, int? newChampionId, int? oldChampionExtraPoints, int? newChampionExtraPoints, int? oldChampionPoints, int? newChampionPoints) : IDomainEvent
    {
        public EnumDomainEvents Type => EnumDomainEvents.RoadChampionChangedEvent;

        public int? OldChampionId = oldChampionId;
        public int? NewChampionId = newChampionId;
        public int? OldChampionExtraPoints = oldChampionExtraPoints;
        public int? NewChampionExtraPoints = newChampionExtraPoints;
        public int? OldChampionPoints = oldChampionPoints;
        public int? NewChampionPoints = newChampionPoints;
    }

    public sealed class KnightChampionChangedEvent(int? oldChampionId, int? newChampionId, int? oldChampionExtraPoints, int? newChampionExtraPoints, int? oldChampionPoints, int? newChampionPoints) : IDomainEvent
    {
        public EnumDomainEvents Type => EnumDomainEvents.KnightChampionChangedEvent;

        public int? OldChampionId = oldChampionId;
        public int? NewChampionId = newChampionId;
        public int? OldChampionExtraPoints = oldChampionExtraPoints;
        public int? NewChampionExtraPoints = newChampionExtraPoints;
        public int? OldChampionPoints = oldChampionPoints;
        public int? NewChampionPoints = newChampionPoints;
    }

    public sealed class ResourcesDistributionDoneEvent(Dictionary<int, Dictionary<EnumResourceType, int>> playersIdsToResources, Dictionary<int, Dictionary<EnumResourceType, int>> playersIdsToResourceChange, 
        Dictionary<EnumResourceType, int> bank) : IDomainEvent
    {
        public EnumDomainEvents Type => EnumDomainEvents.ResourcesDistributionDoneEvent;

        public Dictionary<int, Dictionary<EnumResourceType, int>> PlayersIdsToResources { get; } = playersIdsToResources;
        public Dictionary<int, Dictionary<EnumResourceType, int>> PlayersIdsToResourceChange { get; } = playersIdsToResourceChange;
        public Dictionary<EnumResourceType, int> Bank { get; } = bank;
    }
}