using Catan.Backend.Data;

namespace Catan.Backend.Models
{
    public class CommandResponseDto
    {
        public bool Success { get; set; }
        public EnumGamePhasesDto? NextPhase { get; set; }
        public List<object> UiMessages { get; set; } = new();
        public List<object> DomainMessages { get; set; } = new();
    }
}