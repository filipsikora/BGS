using Catan.Core.Snapshots.ClientQueries;
using Catan.Shared.Dtos;

namespace Catan.Backend.Mappers
{
    public static class BoardMappers
    {
        public static VertexDto MapVertexToDto(VertexSnapshot snapshot)
        {
            return new VertexDto
            {
                Corners = snapshot.Corners.Select(c => new CornerDto
                {
                    HexQ = c.HexQ,
                    HexR = c.HexR,
                    CornerIndex = c.CornerIndex
                }).ToList(),
                VertexId = snapshot.VertexId
            };
        }

        public static FullVertexDto MapFullVertexToDto(FullVertexSnapshot snapshot)
        {
            return new FullVertexDto
            {
                Corners = snapshot.Corners.Select(c => new CornerDto
                {
                    HexQ = c.HexQ,
                    HexR = c.HexR,
                    CornerIndex = c.CornerIndex
                }).ToList(),
                VertexId = snapshot.VertexId,
                Building = snapshot.Building,
                OwnerId = snapshot.OwnerId
            };
        }

        public static EdgeDto MapEdgeToDto(EdgeSnapshot snapshot)
        {
            return new EdgeDto
            {
                EdgeId = snapshot.EdgeId,
                VertexAId = snapshot.VertexAId,
                VertexBId = snapshot.VertexBId
            };
        }

        public static FullEdgeDto MapFullEdgeToDto(FullEdgeSnapshot snapshot)
        {
            return new FullEdgeDto
            {
                EdgeId = snapshot.EdgeId,
                VertexAId = snapshot.VertexAId,
                VertexBId = snapshot.VertexBId,
                OwnerId = snapshot.OwnerId
            };
        }

        public static HexDto MapHexToDto(HexSnapshot snapshot)
        {
            return new HexDto
            {
                HexId = snapshot.HexId,
                HexNumber = snapshot.HexNumber,
                FieldType = snapshot.FieldType,
                Q = snapshot.Q,
                R = snapshot.R
            };
        }

        public static FullBoardDto MapFullBoardToDto(FullBoardSnapshot snapshot)
        {
            return new FullBoardDto
            {
                Vertices = snapshot.Vertices.Select(MapFullVertexToDto).ToList(),
                Edges = snapshot.Edges.Select(MapFullEdgeToDto).ToList(),
                Hexes = snapshot.Hexes.Select(MapHexToDto).ToList(),
                Ports = snapshot.Ports.Select(MapPortToDto).ToList(),
                BlockedHexId = snapshot.BlockedHexId
            };
        }

        public static PortDto MapPortToDto(PortSnapshot snapshot)
        {
            return new PortDto
            {
                EdgeId = snapshot.EdgeId,
                Type = snapshot.Type == null ? null : snapshot.Type
            };
        }

        public static BoardDto MapBoardToDto(BoardSnapshot snapshot)
        {
            return new BoardDto
            {
                Vertices = snapshot.Vertices.Select(MapVertexToDto).ToList(),
                Edges = snapshot.Edges.Select(MapEdgeToDto).ToList(),
                Hexes = snapshot.Hexes.Select(MapHexToDto).ToList(),
                Ports = snapshot.Ports.Select(MapPortToDto).ToList(),
                BlockedHexId = snapshot.BlockedHexId
            };
        }

        public static DevelopmentCardDto MapDevCardToDto(DevelopmentCardSnapshot snapshot)
        {
            return new DevelopmentCardDto
            {
                Type = snapshot.Type,
                Id = snapshot.Id,
                IsNew = snapshot.IsNew,
                IsPlayable = snapshot.IsPlayable
            };
        }
    }
}