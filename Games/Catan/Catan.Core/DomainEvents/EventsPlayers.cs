using Catan.Core.Interfaces;
using Catan.Shared.Data;

namespace Catan.Core.DomainEvents
{
    public sealed class PlayerStateChangedEvent : IDomainEvent
    {
        public int PlayerId;
        public PlayerStateChangedEvent(int playerId)
        {
            PlayerId = playerId;
        }
    }

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
}