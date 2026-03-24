using Catan.Backend.Data;
using System.Text.Json;

namespace Catan.Backend.Models
{
    public class CommandRequestDto
    {
        public EnumCommandType Type { get; set; }
        public JsonElement Data { get; set; }
    }
}
