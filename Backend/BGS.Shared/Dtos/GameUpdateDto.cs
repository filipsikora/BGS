using Newtonsoft.Json.Linq;

namespace BGS.Shared.Dtos
{
    public sealed class GameUpdateDto(string type, Guid playerToken, JToken payload)
    {
        public string Type { get; set; } = type;
        public Guid PlayerToken { get; set; } = playerToken;
        public JToken Payload { get; set; } = payload;
    }
}