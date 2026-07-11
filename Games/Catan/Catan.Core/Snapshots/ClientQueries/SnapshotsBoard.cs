using Catan.Core.Data;
using Catan.Shared.Data;

namespace Catan.Core.Snapshots.ClientQueries
{
    public sealed class FullBoardSnapshot
    {
        public List<FullVertexSnapshot> Vertices;
        public List<FullEdgeSnapshot> Edges;
        public List<HexSnapshot> Hexes;
        public List<PortSnapshot> Ports;
        public int BlockedHexId;

        public FullBoardSnapshot(List<FullVertexSnapshot> vertices, List<FullEdgeSnapshot> edges, List<HexSnapshot> hexes, List<PortSnapshot> ports, int blockedHexId)
        {
            Vertices = vertices;
            Edges = edges;
            Hexes = hexes;
            Ports = ports;
            BlockedHexId = blockedHexId;
        }
    }

    public sealed class FullVertexSnapshot
    {
        public int VertexId;
        public List<(int HexQ, int HexR, int CornerIndex)> Corners;

        public int? OwnerId;
        public EnumBuildings Building;

        public FullVertexSnapshot(int vertexId, List<(int HexQ, int HexR, int CornerIndex)> corners, int? ownerId, EnumBuildings building)
        {
            VertexId = vertexId;
            Corners = corners;
            OwnerId = ownerId;
            Building = building;
        }
    }

    public sealed class FullEdgeSnapshot
    {
        public int EdgeId;
        public int VertexAId;
        public int VertexBId;

        public int? OwnerId;

        public FullEdgeSnapshot(int edgeId, int vertexAId, int vertexBId, int? ownerId)
        {
            EdgeId = edgeId;
            VertexAId = vertexAId;
            VertexBId = vertexBId;
            OwnerId = ownerId;
        }
    }

    public sealed class EdgeSnapshot
    {
        public int EdgeId;
        public int VertexAId;
        public int VertexBId;

        public EdgeSnapshot(int edgeId, int vertexAId, int vertexBId)
        {
            EdgeId = edgeId;
            VertexAId = vertexAId;
            VertexBId = vertexBId;
        }
    }

    public sealed class VertexSnapshot
    {
        public int VertexId;
        public List<(int HexQ, int HexR, int CornerIndex)> Corners;

        public VertexSnapshot(int vertexId, List<(int HexQ, int HexR, int CornerIndex)> corners)
        {
            VertexId = vertexId;
            Corners = corners;
        }
    }

    public sealed class HexSnapshot
    {
        public int HexId;
        public int? HexNumber;
        public EnumFieldTypes? FieldType;
        public int Q;
        public int R;

        public HexSnapshot(int hexId, int? hexNumber, EnumFieldTypes? fieldType, int q, int r)
        {
            HexId = hexId;
            HexNumber = hexNumber;
            FieldType = fieldType;
            Q = q;
            R = r;
        }
    }

    public sealed class BoardSnapshot
    {
        public List<HexSnapshot> Hexes;
        public List<VertexSnapshot> Vertices;
        public List<EdgeSnapshot> Edges;
        public List<PortSnapshot> Ports;
        public int? BlockedHexId;

        public BoardSnapshot(List<HexSnapshot> hexes, List<VertexSnapshot> vertices, List<EdgeSnapshot> edges, List<PortSnapshot> ports, int? blockedHexId)
        {
            Hexes = hexes;
            Vertices = vertices;
            Edges = edges;
            Ports = ports;
            BlockedHexId = blockedHexId;
        }
    }

    public sealed class PortSnapshot
    {
        public int EdgeId;
        public EnumResourceType? Type;

        public PortSnapshot(int edgeId, EnumResourceType? type)
        {
            EdgeId = edgeId;
            Type = type;
        }
    }
}