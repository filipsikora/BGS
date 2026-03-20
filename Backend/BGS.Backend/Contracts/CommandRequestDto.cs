using BGS.Backend.Contracts.Data;
using System.Text.Json;

namespace BGS.Backend.Contracts
{
    public class CommandRequestDto
    {
        public EnumCommandType Type { get; set; }
        public JsonElement Data { get; set; }
    }
}
