using Catan.Core.Interfaces;
using Catan.Shared.Data;

namespace Catan.Core.DomainEvents
{
    public sealed class RolledNumberChangedEvent : IDomainEvent
    {
        public EnumDomainEvents Type => EnumDomainEvents.DiceRolledEvent;
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

    public sealed class PhaseChangedEvent(EnumGamePhases phase, List<int> playersToMove) : IDomainEvent
    {
        public EnumDomainEvents Type => EnumDomainEvents.PhaseChangedEvent;
        public EnumGamePhases Phase = phase;
        public List<int> PlayersToMove = playersToMove;
    }
}