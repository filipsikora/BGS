using Catan.Shared.Interfaces;

namespace Catan.Shared.Dtos.UiMessages
{
    public sealed class BuildOptionsSentDto : IUiMessageDto
    {
        public bool CanBuildVillage { get; }
        public bool CanBuildRoad { get; }
        public bool CanUpgradeVillage { get; }

        public BuildOptionsSentDto(bool canVillage, bool canRoad, bool canTown)
        {
            CanBuildVillage = canVillage;
            CanBuildRoad = canRoad;
            CanUpgradeVillage = canTown;
        }
    }

    public sealed class VillagePlacedMessageDto : IUiMessageDto
    {
        public int VertexId;
        public int OwnerId;
        public VillagePlacedMessageDto(int vertexId, int ownerId)
        {
            VertexId = vertexId;
            OwnerId = ownerId;
        }
    }

    public sealed class RoadPlacedMessageDto : IUiMessageDto
    {
        public int EdgeId;
        public int OwnerId;
        public RoadPlacedMessageDto(int edgeId, int ownerId)
        {
            EdgeId = edgeId;
            OwnerId = ownerId;
        }
    }

    public sealed class TownPlacedMessageDto : IUiMessageDto
    {
        public int VertexId;
        public int OwnerId;
        public TownPlacedMessageDto(int vertexId, int ownerId)
        {
            VertexId = vertexId;
            OwnerId = ownerId;
        }
    }
}