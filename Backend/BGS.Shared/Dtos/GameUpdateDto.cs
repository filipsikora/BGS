using Newtonsoft.Json.Linq;

namespace BGS.Shared.Dtos
{
    public sealed class GameUpdateDto(string dtoType, Guid playerToken, JToken payload)
    {
        public string DtoType { get; set; } = dtoType;
        public Guid PlayerToken { get; set; } = playerToken;
        public JToken Payload { get; set; } = payload;
    }
}