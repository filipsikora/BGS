#nullable enable
using Catan.Shared.Data;
using System.Collections.Generic;

namespace Catan.Core.Models
{
    public class HexTile
    {
        public int Q { get; }

        public int R { get; }

        public int S => -Q - R;

        public int Id { get; set; }

        public float X { get; set; }

        public float Y { get; set; }


        public EnumFieldTypes? FieldType { get; set; } = null;

        public int? FieldNumber { get; set; } = null;

        public List<Vertex> AdjacentVertices { get; set; } = new List<Vertex>();

        public bool isBlocked = false;


        public HexTile(int q, int r, int id)
        {
            Q = q; 
            R = r;
            Id = id;
        }


        public override string ToString()
        {
            return $"Hex ({Q}, {R}) - {FieldNumber}, {FieldType}";
        }

        public EnumResourceType? GetResourceType()
        {
            return FieldType switch
            {
                EnumFieldTypes.Wheat => EnumResourceType.Wheat,
                EnumFieldTypes.Wood => EnumResourceType.Wood,
                EnumFieldTypes.Wool => EnumResourceType.Wool,
                EnumFieldTypes.Stone => EnumResourceType.Stone,
                EnumFieldTypes.Clay => EnumResourceType.Clay,
                EnumFieldTypes.Desert => null,
                _ => null
            };
        }

    }
}
