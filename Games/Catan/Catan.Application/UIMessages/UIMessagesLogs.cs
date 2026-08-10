using Catan.Application.Interfaces;
using Catan.Shared.Data;

namespace Catan.Application.UIMessages
{
    public sealed class ActionRejectedMessage : IUIMessages
    {
        public int PlayerId { get; }
        public ConditionFailureReason Reason { get; }
        public ActionRejectedMessage(int playerId, ConditionFailureReason reason)
        {
            PlayerId = playerId;
            Reason = reason;
        }
    }
}