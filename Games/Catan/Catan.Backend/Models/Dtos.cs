using Catan.Backend.Helpers;
using Catan.Shared.Data;
using Catan.Shared.Interfaces;

namespace Catan.Backend.Models
{
    public class BankTradeOfferedResourceSelectedDto : IValidatableDto
    {
        public  EnumResourceType? Type { get; set; }

        public void Validate() => DtoValidation.RequireValueNotNull(Type, nameof(Type));
    }

    public class BankTradeDesiredResourceSelectedDto : IValidatableDto
    {
        public EnumResourceType? Type { get; set; }

        public void Validate() {} // this command is always valid (accepts nullable)
    }

    public class VertexClickedDto : IValidatableDto
    {
        public int? VertexId { get; set; }

        public void Validate() => DtoValidation.RequirePositiveInt(VertexId, nameof(VertexId));
    }

    public class EdgeClickedDto : IValidatableDto
    {
        public int? EdgeId { get; set; }

        public void Validate() => DtoValidation.RequirePositiveInt(EdgeId, nameof(EdgeId));
    }

    public class HexClickedDto : IValidatableDto
    {
        public int? HexId { get; set; }

        public void Validate() => DtoValidation.RequirePositiveInt(HexId, nameof(HexId));
    }

    public class DevelopmentCardClickedPlayedDto : IValidatableDto
    {
        public int? DevelopmentCardId { get; set; }

        public void Validate() => DtoValidation.RequirePositiveInt(DevelopmentCardId, nameof(DevelopmentCardId));
    }

    public class ResourceCardSelectedDto : IValidatableDto
    {
        public EnumResourceType? Type { get; set; }
        public bool? IsSelected { get; set; }

        public void Validate()
        {
            DtoValidation.RequireValueNotNull(Type, nameof(Type));
            DtoValidation.RequireValueNotNull(IsSelected, nameof(IsSelected));
        }
    }

    public class VictimChosenDto : IValidatableDto
    {
        public int? VictimId { get; set; }

        public void Validate() => DtoValidation.RequirePositiveInt(VictimId, nameof(VictimId));
    }

    public class StolenCardSelectedDto : IValidatableDto
    {
        public EnumResourceType? Type { get; set; }

        public void Validate() => DtoValidation.RequireValueNotNull(Type, nameof(Type));
    }

    public class TradePartnerChosenDto : IValidatableDto
    {
        public int? PlayerId { get; set; }

        public void Validate() => DtoValidation.RequirePositiveInt(PlayerId, nameof(PlayerId));
    }

    public class EmptyDto : IValidatableDto
    {
        public void Validate() { }
    }
}