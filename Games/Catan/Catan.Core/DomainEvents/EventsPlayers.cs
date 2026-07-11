using Catan.Core.Interfaces;
using Catan.Core.Models;

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