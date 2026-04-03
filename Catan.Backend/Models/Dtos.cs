using Catan.Backend.Helpers;
using Catan.Shared.Data;
using Catan.Shared.Interfaces;

namespace Catan.Backend.Models
{
    public class BankTradeOfferedResourceSelectedDto : IValidatableDto
    {
        public  EnumResourceType? Type { get; set; }

        public EnumResourceType GetValidatedData()
        {
            return DtoValidation.RequireValueNotNull(Type, nameof(Type));
        }

        public void Validate() => GetValidatedData();
    }

    public class BankTradeDesiredResourceSelectedDto : IValidatableDto
    {
        public EnumResourceType? Type { get; set; }

        public EnumResourceType? GetValidatedData()
        {
            return Type;
        }
        public void Validate() => GetValidatedData();
    }


    public class VertexClickedDto : IValidatableDto
    {
        public int? VertexId { get; set; }

        public int GetValidatedData()
        {
            return DtoValidation.RequirePositiveInt(VertexId, nameof(VertexId));
        }

        public void Validate() => GetValidatedData();
    }

    public class EdgeClickedDto : IValidatableDto
    {
        public int? EdgeId { get; set; }

        public int GetValidatedData()
        {
            return DtoValidation.RequirePositiveInt(EdgeId, nameof(EdgeId));
        }

        public void Validate() => GetValidatedData();
    }

    public class HexClickedDto : IValidatableDto
    {
        public int? HexId { get; set; }

        public int GetValidatedData()
        {
            return DtoValidation.RequirePositiveInt(HexId, nameof(HexId));
        }

        public void Validate() => GetValidatedData();
    }

    public class DevelopmentCardClickedPlayedDto : IValidatableDto
    {
        public int? DevelopmentCardId { get; set; }

        public int GetValidatedData()
        {
            return DtoValidation.RequirePositiveInt(DevelopmentCardId, nameof(DevelopmentCardId));
        }

        public void Validate() => GetValidatedData();
    }

    public class ResourceCardSelectedDto : IValidatableDto
    {
        public EnumResourceType? Type { get; set; }
        public bool? IsSelected { get; set; }

        public (EnumResourceType, bool) GetValidatedData()
        {
            var typeValidated = DtoValidation.RequireValueNotNull(Type, nameof(Type));
            var isSelectedValidated = DtoValidation.RequireValueNotNull(IsSelected, nameof(IsSelected));

            return (typeValidated, isSelectedValidated);
        }

        public void Validate() => GetValidatedData();
    }

    public class VictimChosenDto : IValidatableDto
    {
        public int? VictimId { get; set; }

        public int GetValidatedData()
        {
            return DtoValidation.RequirePositiveInt(VictimId, nameof(VictimId));
        }

        public void Validate() => GetValidatedData();
    }

    public class StolenCardSelectedDto : IValidatableDto
    {
        public EnumResourceType? Type { get; set; }

        public EnumResourceType GetValidatedData()
        {
            return DtoValidation.RequireValueNotNull(Type, nameof(Type));
        }

        public void Validate() => GetValidatedData();
    }

    public class TradePartnerChosenDto : IValidatableDto
    {
        public int? PlayerId { get; set; }

        public int GetValidatedData()
        {
            return DtoValidation.RequirePositiveInt(PlayerId, nameof(PlayerId));
        }

        public void Validate() => GetValidatedData();
    }

    public class EmptyDto : IValidatableDto
    {
        public void Validate() { }
    }
}
