using Catan.Core.Models;
using Catan.Core.Snapshots.ClientQueries;

namespace Catan.Core.Runtime
{
    public sealed class BoardStateReader
    {
        public BoardStateReader() { }

        public List<(int HexQ, int HexR, int CornerIndex)> GetVertexCorners(Vertex vertex)
        {
            var corners = new List<(int, int, int)>();

            foreach (var hex in vertex.AdjacentHexTiles)
            {
                var cornerIndex = hex.AdjacentVertices.IndexOf(vertex);

                if (cornerIndex >= 0)
                {
                    corners.Add((hex.Q, hex.R, cornerIndex));
                }
            }

            return corners;
        }

        public VertexSnapshot GetVertexData(Vertex vertex)
        {
            var corners = new List<(int HexQ, int HexR, int CornerIndex)>();

            foreach (var hex in vertex.AdjacentHexTiles)
            {
                var cornerIndex = hex.AdjacentVertices.IndexOf(vertex);

                if (cornerIndex >= 0)
                {
                    corners.Add((hex.Q, hex.R, cornerIndex));
                }
            }

            return new VertexSnapshot(vertex.Id, corners);
        }

        public EdgeSnapshot GetEdgeData(Edge edge)
        {
            return new EdgeSnapshot(edge.Id, edge.VertexA.Id, edge.VertexB.Id);
        }

        public HexSnapshot GetHexData(HexTile hex)
        {
            var hexNumber = hex.FieldNumber;
            var hexType = hex.FieldType;
            var hexQ = hex.Q;
            var hexR = hex.R;

            return new HexSnapshot(hex.Id, hexNumber, hexType, hexQ, hexR);
        }

        public PortSnapshot GetPortData(Edge edge, Port port)
        {
            return new PortSnapshot(edge.Id, port.Type);
        }

        public List<int> GetAdjacentToHexPlayersIds(HexTile hex)
        {
            List<int> adjacentPlayersIds = new();

            foreach (var vertex in hex.AdjacentVertices)
            {
                Player? owner = vertex.Owner;

                if (vertex.IsOwned && !adjacentPlayersIds.Contains(owner.ID))
                {
                    adjacentPlayersIds.Add(owner.ID);
                }
            }

            return adjacentPlayersIds;
        }
    }
}
