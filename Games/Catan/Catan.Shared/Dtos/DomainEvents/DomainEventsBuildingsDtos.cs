using Catan.Shared.Data;

namespace Catan.Shared.Dtos.DomainEvents
{
    public sealed class VillagePlacedEventPrivateDto(int vertexId, int ownerId, int points, int villagesLeft, Dictionary<EnumResourceType, int> resources)
    {
        public int VertexId = vertexId;
        public int OwnerId = ownerId;
        public int Points = points;
        public int VillagesLeft = villagesLeft;
        public Dictionary<EnumResourceType, int> Resources = resources;
    }

    public sealed class VillagePlacedEventPublicDto(int vertexId, int ownerId, int points, int villagesLeft, int resourcesCount)
    {
        public int VertexId = vertexId;
        public int OwnerId = ownerId;
        public int Points = points;
        public int VillagesLeft = villagesLeft;
        public int ResourcesCount = resourcesCount;
    }

    public sealed class RoadPlacedEventPrivateDto(int edgeId, int ownerId, int roadsLeft, Dictionary<EnumResourceType, int> resources)
    {
        public int EdgeId = edgeId;
        public int OwnerId = ownerId;
        public int RoadsLeft = roadsLeft;
        public Dictionary<EnumResourceType, int> Resources = resources;
    }

    public sealed class RoadPlacedEventPublicDto(int edgeId, int ownerId, int roadsLeft, int resourcesCount)
    {
        public int EdgeId = edgeId;
        public int OwnerId = ownerId;
        public int RoadsLeft = roadsLeft;
        public int ResourcesCount = resourcesCount;
    }
}
