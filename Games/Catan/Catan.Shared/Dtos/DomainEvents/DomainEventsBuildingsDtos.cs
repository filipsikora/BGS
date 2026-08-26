using Catan.Shared.Data;
using Catan.Shared.Interfaces;

namespace Catan.Shared.Dtos.DomainEvents
{
    public sealed class VillagePlacedEventPrivateDto(
        int vertexId,
        int ownerId,
        int points,
        Dictionary<EnumResourceType, int> resources,
        Dictionary<EnumResourceType, int> bank,
        int resourcesCount,
        Dictionary<string, int> buildingsLeft) : IDomainEventDto
    {
        public int VertexId = vertexId;
        public int OwnerId = ownerId;
        public int Points = points;
        public Dictionary<EnumResourceType, int> Resources = resources;
        public int ResourcesCount = resourcesCount;
        public Dictionary<EnumResourceType, int> Bank = bank;
        public Dictionary<string, int> BuildingsLeft = buildingsLeft;
    }

    public sealed class VillagePlacedEventPublicDto(
        int vertexId,
        int ownerId,
        int points,
        int resourcesCount,
        Dictionary<EnumResourceType, int> bank,
        Dictionary<string, int> buildingsLeft) : IDomainEventDto
    {
        public int VertexId = vertexId;
        public int OwnerId = ownerId;
        public int Points = points;
        public int ResourcesCount = resourcesCount;
        public Dictionary<EnumResourceType, int> Bank = bank;
        public Dictionary<string, int> BuildingsLeft = buildingsLeft;
    }

    public sealed class RoadPlacedEventPrivateDto(
        int edgeId,
        int ownerId,
        Dictionary<EnumResourceType, int> resources,
        Dictionary<EnumResourceType, int> bank,
        int resourcesCount,
        Dictionary<string, int> buildingsLeft) : IDomainEventDto
    {
        public int EdgeId = edgeId;
        public int OwnerId = ownerId;
        public Dictionary<EnumResourceType, int> Resources = resources;
        public int ResourcesCount = resourcesCount;
        public Dictionary<EnumResourceType, int> Bank = bank;
        public Dictionary<string, int> BuildingsLeft = buildingsLeft;
    }

    public sealed class RoadPlacedEventPublicDto(
        int edgeId,
        int ownerId,
        int resourcesCount,
        Dictionary<EnumResourceType, int> bank,
        Dictionary<string, int> buildingsLeft) : IDomainEventDto
    {
        public int EdgeId = edgeId;
        public int OwnerId = ownerId;
        public int ResourcesCount = resourcesCount;
        public Dictionary<EnumResourceType, int> Bank = bank;
        public Dictionary<string, int> BuildingsLeft = buildingsLeft;
    }

    public sealed class TownPlacedEventPrivateDto(
        int vertexId,
        int ownerId,
        int points,
        Dictionary<EnumResourceType, int> resources,
        Dictionary<EnumResourceType, int> bank,
        int resourcesCount,
        Dictionary<string, int> buildingsLeft) : IDomainEventDto
    {
        public int VertexId = vertexId;
        public int OwnerId = ownerId;
        public int Points = points;
        public Dictionary<EnumResourceType, int> Resources = resources;
        public int ResourcesCount = resourcesCount;
        public Dictionary<EnumResourceType, int> Bank = bank;
        public Dictionary<string, int> BuildingsLeft = buildingsLeft;
    }

    public sealed class TownPlacedEventPublicDto(
        int vertexId,
        int ownerId,
        int points,
        int resourcesCount,
        Dictionary<EnumResourceType, int> bank,
        Dictionary<string, int> buildingsLeft) : IDomainEventDto
    {
        public int VertexId = vertexId;
        public int OwnerId = ownerId;
        public int Points = points;
        public int ResourcesCount = resourcesCount;
        public Dictionary<EnumResourceType, int> Bank = bank;
        public Dictionary<string, int> BuildingsLeft = buildingsLeft;
    }
}