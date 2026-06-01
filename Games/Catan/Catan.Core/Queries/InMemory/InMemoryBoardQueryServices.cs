using Catan.Core.Queries.Interfaces;
using Catan.Core.Runtime;
using Catan.Core.Snapshots.ClientQueries;

namespace Catan.Core.Queries.InMemory
{
    public sealed class InMemoryBoardQueryServices : IBoardQueryService
    {
        private readonly GameSession _session;

        public InMemoryBoardQueryServices(GameSession session)
        {
            _session = session;
        }

        public EdgeSnapshot GetEdgeData(int edgeId) => _session.GetEdgeData(edgeId);

        public VertexSnapshot GetVertexData(int vertexId) => _session.GetVertexData(vertexId);

        public HexSnapshot GetHexData(int hexId) => _session.GetHexData(hexId);

        public PortSnapshot GetPortData(int edgeId) => _session.GetPortData(edgeId);

        public BoardSnapshot GetBoardData()
        {
            var HexesSnapshotsList = new List<HexSnapshot>();
            var VerticesSnapshotsList = new List<VertexSnapshot>();
            var EdgesSnapshotsList = new List<EdgeSnapshot>();
            var PortsSnapshotList = new List<PortSnapshot>();

            foreach (var hex in _session.GetAllHexTilesView())
            {
                var hexData = GetHexData(hex.Id);
                HexesSnapshotsList.Add(hexData);
            }

            foreach (var vertex in _session.GetAllVerticesView())
            {
                var vertexData = GetVertexData(vertex.Id);
                VerticesSnapshotsList.Add(vertexData);
            }

            foreach (var edge in _session.GetAllEdgesView())
            {
                var edgeData = GetEdgeData(edge.Id);
                EdgesSnapshotsList.Add(edgeData);
            }

            foreach (var port in _session.GetAllPortsView())
            {
                var edge = port.Edge;
                var portData = GetPortData(edge.Id);
                PortsSnapshotList.Add(portData);
            }

            return new BoardSnapshot(HexesSnapshotsList, VerticesSnapshotsList, EdgesSnapshotsList, PortsSnapshotList, _session.GetBlockedHexId());
        }
    }
}