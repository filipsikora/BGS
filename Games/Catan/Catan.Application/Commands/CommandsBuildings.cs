using Catan.Application.Interfaces;

namespace Catan.Application.Commands
{
    public class BuildVillageCommand : ICommand
    {
        public int VertexId;
        public BuildVillageCommand(int vertexId)
        {
            VertexId = vertexId;
        }
    }

    public class BuildRoadCommand : ICommand
    {
        public int EdgeId;
        public BuildRoadCommand(int edgeId)
        {
            EdgeId = edgeId;
        }
    }

    public class UpgradeVillageCommand : ICommand
    {
        public int VertexId;
        public UpgradeVillageCommand(int vertexId)
        {
            VertexId = vertexId;
        }
    }
}