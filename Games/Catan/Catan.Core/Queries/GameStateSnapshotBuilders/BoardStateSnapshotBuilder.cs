using Catan.Core.Data;
using Catan.Core.Models;
using Catan.Core.Runtime;
using Catan.Core.Snapshots.ClientQueries;

namespace Catan.Core.Queries.GameStateSnapshotBuilders
{
    public class BoardStateSnapshotBuilder
    {
        private readonly GameSession _session;

        public BoardStateSnapshotBuilder(GameSession session)
        {
            _session = session;
        }

        public FullBoardSnapshot GetFullBoardData()
        {
            return new FullBoardSnapshot(GetFullVertices(), GetFullEdges(), GetHexes(), GetPorts(), _session.GetBlockedHexId());
        }

        private List<FullVertexSnapshot> GetFullVertices()
        {
            var fullVertices = new List<FullVertexSnapshot>();
            var vertices = _session.GetAllVerticesView();

            foreach (var vertex in vertices)
            {
                fullVertices.Add(GetFullVertex(vertex));
            }

            return fullVertices;
        }

        private FullVertexSnapshot GetFullVertex(Vertex vertex)
        {
            var corners = _session.GetVertexCorners(vertex.Id);

            int? ownerId = vertex.Owner != null ? vertex.Owner.ID : null;
            var building = vertex.HasVillage ? EnumBuildings.Village : vertex.HasTown ? EnumBuildings.Town : EnumBuildings.None;

            return new FullVertexSnapshot(vertex.Id, corners, ownerId, building);
        }

        private List<FullEdgeSnapshot> GetFullEdges()
        {
            var fullEdges = new List<FullEdgeSnapshot>();
            var edges = _session.GetAllEdgesView();

            foreach (var edge in edges)
            {
                fullEdges.Add(GetFullEdge(edge));
            }

            return fullEdges;
        }

        private FullEdgeSnapshot GetFullEdge(Edge edge)
        {
            int? ownerId = edge.Owner != null ? edge.Owner.ID : null;

            return new FullEdgeSnapshot(edge.Id, edge.VertexA.Id, edge.VertexB.Id, ownerId);
        }

        private List<HexSnapshot> GetHexes()
        {
            var hexes = new List<HexSnapshot>();
            var hexesList = _session.GetAllHexTilesView();

            foreach (var hex in hexesList)
            {
                hexes.Add(_session.GetHexData(hex.Id));
            }

            return hexes;
        }

        private List<PortSnapshot> GetPorts()
        {
            var ports = new List<PortSnapshot>();
            var portsList = _session.GetAllPortsView();

            foreach (var port in portsList)
            {
                ports.Add(_session.GetPortData(port.Edge.Id));
            }

            return ports;
        }
    }
}