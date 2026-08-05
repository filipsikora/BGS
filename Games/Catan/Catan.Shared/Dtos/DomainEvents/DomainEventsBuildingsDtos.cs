using Catan.Shared.Data;

namespace Catan.Shared.Dtos.DomainEvents
{
    public sealed class VillagePlacedEventPrivateDto(int vertexId, int ownerId, int points, int villagesLeft, Dictionary<EnumResourceType, int> resources, Dictionary<EnumResourceType, int> bank)
    {
        public int VertexId = vertexId;
        public int OwnerId = ownerId;
        public int Points = points;
        public int VillagesLeft = villagesLeft;
        public Dictionary<EnumResourceType, int> Resources = resources;
        public Dictionary<EnumResourceType, int> Bank = bank;
    }

    public sealed class VillagePlacedEventPublicDto(int vertexId, int ownerId, int points, int villagesLeft, int resourcesCount, Dictionary<EnumResourceType, int> bank)
    {
        public int VertexId = vertexId;
        public int OwnerId = ownerId;
        public int Points = points;
        public int VillagesLeft = villagesLeft;
        public int ResourcesCount = resourcesCount;
        public Dictionary<EnumResourceType, int> Bank = bank;
    }

    public sealed class RoadPlacedEventPrivateDto(int edgeId, int ownerId, int roadsLeft, Dictionary<EnumResourceType, int> resources, Dictionary<EnumResourceType, int> bank)
    {
        public int EdgeId = edgeId;
        public int OwnerId = ownerId;
        public int RoadsLeft = roadsLeft;
        public Dictionary<EnumResourceType, int> Resources = resources;
        public Dictionary<EnumResourceType, int> Bank = bank;
    }

    public sealed class RoadPlacedEventPublicDto(int edgeId, int ownerId, int roadsLeft, int resourcesCount, Dictionary<EnumResourceType, int> bank)
    {
        public int EdgeId = edgeId;
        public int OwnerId = ownerId;
        public int RoadsLeft = roadsLeft;
        public int ResourcesCount = resourcesCount;
        public Dictionary<EnumResourceType, int> Bank = bank;
    }

    public sealed class TownPlacedEventPrivateDto(int vertexId, int ownerId, int points, int townsLeft, int villagesLeft, Dictionary<EnumResourceType, int> resources, Dictionary<EnumResourceType, int> bank)
    {
        public int VertexId = vertexId;
        public int OwnerId = ownerId;
        public int Points = points;
        public int TownsLeft = townsLeft;
        public int VillagesLeft = villagesLeft;
        public Dictionary<EnumResourceType, int> Resources = resources;
        public Dictionary<EnumResourceType, int> Bank = bank;
    }

    public sealed class TownPlacedEventPublicDto(int vertexId, int ownerId, int points, int townsLeft, int villagesLeft, int resourcesCount, Dictionary<EnumResourceType, int> bank)
    {
        public int VertexId = vertexId;
        public int OwnerId = ownerId;
        public int Points = points;
        public int TownsLeft = townsLeft;
        public int VillagesLeft = villagesLeft;
        public int ResourcesCount = resourcesCount;
        public Dictionary<EnumResourceType, int> Bank = bank;
    }
}
