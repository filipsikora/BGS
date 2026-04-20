using Catan.Core.Interfaces;

namespace Catan.Core.DomainEvents
{
    public sealed class RolledNumberChangedEvent : IDomainEvent
    {
        public int NewRolledNumber;
        public RolledNumberChangedEvent(int newRolledNumber)
        {
            NewRolledNumber = newRolledNumber;
        }
    }

    public sealed class TurnNumberChangedEvent : IDomainEvent
    {
        public int NewTurnNumber;
        public TurnNumberChangedEvent(int newTurnNumber)
        {
            NewTurnNumber = newTurnNumber;
        }
    }
}