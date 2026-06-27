using BGS.Shared.Data;

namespace BGS.GameAbstractions.Models
{
    public class JoinResult
    {
        public EnumJoinStatus JoinStatus { get; }
        public string? Message { get; }
        public Guid? PlayerToken { get; }
        public object? InitialState { get; }

        public JoinResult(EnumJoinStatus joinStatus, string message, Guid? playerToken, object? initialState)
        {
            JoinStatus = joinStatus;
            Message = message;
            PlayerToken = playerToken;
            InitialState = initialState;
        }
    }
}