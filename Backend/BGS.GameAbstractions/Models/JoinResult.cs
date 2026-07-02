using BGS.Shared.Data;
using Newtonsoft.Json.Linq;

namespace BGS.GameAbstractions.Models
{
    public class JoinResult
    {
        public EnumJoinStatus JoinStatus { get; }
        public string? Message { get; }
        public Guid? PlayerToken { get; }
        public JToken? Payload { get; }

        public JoinResult(EnumJoinStatus joinStatus, string message, Guid? playerToken, JToken? initialState)
        {
            JoinStatus = joinStatus;
            Message = message;
            PlayerToken = playerToken;
            Payload = initialState;
        }
    }
}