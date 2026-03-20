using BGS.Backend.Contracts.Data;

namespace BGS.Backend.Contracts
{
    public class BankTradeOfferedResourceSelectedDto
    {
        public  EnumResourceTypesDto Type { get; set; }
    }

    public class BankTradeDesiredResourceSelectedDto
    {
        public EnumResourceTypesDto? Type { get; set; }
    }

    public class VertexClickedDto
    {
        public int VertexId { get; set; }
    }

    public class EdgeClickedDto
    {
        public int EdgeId { get; set; }
    }

    public class HexClickedDto
    {
        public int HexId { get; set; }
    }

    public class DevelopmentCardClickedPlayedDto
    {
        public int DevelopmentCardId { get; set; }
    }

    public class ResourceCardSelectedDto
    {
        public EnumResourceTypesDto Type { get; set; }
        public bool IsSelected { get; set; }
    }

    public class VictimChosenDto
    {
        public int VictimId { get; set; }
    }

    public class StolenCardSelectedDto
    {
        public EnumResourceTypesDto Type { get; set; }
    }

    public class TradePartnerChosenDto
    {
        public int PlayerId { get; set; }
    }

    public class EmptyDto { }
}
