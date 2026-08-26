using Catan.Core.Interfaces;
using Catan.Shared.Data;

namespace Catan.Core.DomainEvents
{
    public sealed class VillagePlacedEvent(int vertexId, int ownerId, int points, Dictionary<EnumResourceType, int> resources, Dictionary<EnumResourceType, int> bank,
        Dictionary<string, int> buildingsLeft) : IDomainEvent
    {
        public EnumDomainEvents Type => EnumDomainEvents.VillagePlacedEvent;
        public int VertexId = vertexId;
        public int OwnerId = ownerId;
        public int Points = points;
        public Dictionary<EnumResourceType, int> Resources = resources;
        public Dictionary<EnumResourceType, int> Bank = bank;
        public Dictionary<string, int> BuildingsLeft = buildingsLeft;
    }

    public sealed class RoadPlacedEvent(int edgeId, int ownerId, Dictionary<EnumResourceType, int> resources, Dictionary<EnumResourceType, int> bank,
        Dictionary<string, int> buildingsLeft) : IDomainEvent
    {
        public EnumDomainEvents Type => EnumDomainEvents.RoadPlacedEvent;
        public int EdgeId = edgeId;
        public int OwnerId = ownerId;
        public Dictionary<EnumResourceType, int> Resources = resources;
        public Dictionary<EnumResourceType, int> Bank = bank;
        public Dictionary<string, int> BuildingsLeft = buildingsLeft;
    }

    public sealed class TownPlacedEvent(int vertexId, int ownerId, int points, Dictionary<EnumResourceType, int> resources, Dictionary<EnumResourceType, int> bank,
        Dictionary<string, int> buildingsLeft) : IDomainEvent
    {
        public EnumDomainEvents Type => EnumDomainEvents.TownPlacedEvent;

        public int VertexId = vertexId;
        public int OwnerId = ownerId;
        public int Points = points;
        public Dictionary<EnumResourceType, int> Resources = resources;
        public Dictionary<EnumResourceType, int> Bank = bank;
        public Dictionary<string, int> BuildingsLeft = buildingsLeft;
    }
}