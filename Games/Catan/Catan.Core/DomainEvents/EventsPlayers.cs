using Catan.Core.Interfaces;

namespace Catan.Core.DomainEvents
{
    public sealed class PlayerStateChangedEvent : IDomainEvent
    {
        public int PlayerId;
        public PlayerStateChangedEvent(int playerId)
        {
            PlayerId = playerId;
        }
    }
}