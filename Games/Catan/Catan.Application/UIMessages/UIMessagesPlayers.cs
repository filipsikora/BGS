using Catan.Application.Interfaces;

namespace Catan.Application.UIMessages
{
    public sealed class PlayerStateChangedMessage : IUIMessages
    {
        public int PlayerId;
        public PlayerStateChangedMessage(int playerId)
        {
            PlayerId = playerId;
        }
    }
}
