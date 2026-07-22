using Catan.Core.Interfaces;
using Catan.Shared.Data;

namespace Catan.Core.DomainEvents
{
    public sealed class VillagePlacedEvent(int vertexId, int ownerId, int points, int villagesLeft, Dictionary<EnumResourceType, int> resources) : IDomainEvent
    {
        public EnumDomainEvents Type => EnumDomainEvents.VillagePlacedEvent;
        public int VertexId = vertexId;
        public int OwnerId = ownerId;
        public int Points = points;
        public int VillagesLeft = villagesLeft;
        public Dictionary<EnumResourceType, int> Resources = resources;
    }

    public sealed class RoadPlacedEvent(int edgeId, int ownerId, int roadsLeft, Dictionary<EnumResourceType, int> resources) : IDomainEvent
    {
        public EnumDomainEvents Type => EnumDomainEvents.RoadPlacedEvent;
        public int EdgeId = edgeId;
        public int OwnerId = ownerId;
        public int RoadsLeft = roadsLeft;
        public Dictionary<EnumResourceType, int> Resources = resources;
    }

    public sealed class TownPlacedEvent : IDomainEvent
    {
        public int VertexId;
        public int OwnerId;
        public TownPlacedEvent(int vertexId, int ownerId)
        {
            VertexId = vertexId;
            OwnerId = ownerId;
        }
    }
}