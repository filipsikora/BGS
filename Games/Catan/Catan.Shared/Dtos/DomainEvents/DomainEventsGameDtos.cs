using Catan.Shared.Data;
using Catan.Shared.Interfaces;

namespace Catan.Shared.Dtos.DomainEvents
{
    public sealed class RolledNumberChangedEventDto(int newRolledNumber) : IDomainEventDto
    {
        public int NewRolledNumber = newRolledNumber;
    }

    public sealed class PhaseChangedEventDto(EnumGamePhases phase, List<int> playersToMove) : IDomainEventDto
    {
        public EnumGamePhases Phase = phase;
        public List<int> PlayersToMove = playersToMove;
    }

    public sealed class PlayersToMoveChangedEventDto(List<int> playersToMove) : IDomainEventDto
    {
        public List<int> PlayersToMove = playersToMove;
    }

    public sealed class GameWonEventDto(int playerId, Dictionary<int, int> playerScoresToIds) : IDomainEventDto
    {
        public int PlayerId = playerId;
        public Dictionary<int, int> PlayerScoresToIds = playerScoresToIds;
    }
}
