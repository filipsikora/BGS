using Catan.Core.Interfaces;
using Catan.Shared.Data;

namespace Catan.Core.DomainEvents
{
    public sealed class RolledNumberChangedEvent(int newRolledNumber) : IDomainEvent
    {
        public EnumDomainEvents Type => EnumDomainEvents.RolledNumberChangedEvent;
        public int NewRolledNumber = newRolledNumber;
    }

    public sealed class GameWonEvent(int playerId, Dictionary<int, int> playerScoresToIds) : IDomainEvent
    {
        public EnumDomainEvents Type => EnumDomainEvents.GameWonEvent;

        public int PlayerId = playerId;
        public Dictionary<int, int> PlayerScoresToIds = playerScoresToIds;
    }

    public sealed class PhaseChangedEvent(EnumGamePhases phase, List<int> playersToMove) : IDomainEvent
    {
        public EnumDomainEvents Type => EnumDomainEvents.PhaseChangedEvent;
        public EnumGamePhases Phase = phase;
        public List<int> PlayersToMove = playersToMove;
    }

    public sealed class PlayersToMoveChangedEvent(List<int> playersToMove) : IDomainEvent
    {
        public EnumDomainEvents Type => EnumDomainEvents.PlayersToMoveChangedEvent;
        public List<int> PlayersToMove = playersToMove;
    }
}