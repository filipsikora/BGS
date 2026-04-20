using Catan.Application.Interfaces;

namespace Catan.Application.UIMessages
{
    public sealed class BuildOptionsSentMessage : IUIMessages
    {
        public bool CanBuildVillage { get; }
        public bool CanBuildRoad { get; }
        public bool CanUpgradeVillage { get; }

        public BuildOptionsSentMessage(bool canVillage, bool canRoad, bool canTown)
        {
            CanBuildVillage = canVillage;
            CanBuildRoad = canRoad;
            CanUpgradeVillage = canTown;
        }
    }

    public sealed class VillagePlacedMessage : IUIMessages
    {
        public int VertexId;
        public int OwnerId;
        public VillagePlacedMessage(int vertexId, int ownerId)
        {
            VertexId = vertexId;
            OwnerId = ownerId;
        }
    }

    public sealed class RoadPlacedMessage : IUIMessages
    {
        public int EdgeId;
        public int OwnerId;
        public RoadPlacedMessage(int edgeId, int ownerId)
        {
            EdgeId = edgeId;
            OwnerId = ownerId;
        }
    }

    public sealed class TownPlacedMessage : IUIMessages
    {
        public int VertexId;
        public int OwnerId;
        public TownPlacedMessage(int vertexId, int ownerId)
        {
            VertexId = vertexId;
            OwnerId = ownerId;
        }
    }
}