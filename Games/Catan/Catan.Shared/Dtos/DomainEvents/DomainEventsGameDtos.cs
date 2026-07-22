using Catan.Shared.Data;

namespace Catan.Shared.Dtos.DomainEvents
{
    public sealed class RolledNumberChangedEventDto(int newRolledNumber)
    {
        public int NewRolledNumber = newRolledNumber;
    }

    public sealed class PhaseChangedEventDto(EnumGamePhases phase, List<int> playersToMove)
    {
        public EnumGamePhases Phase = phase;
        public List<int> PlayersToMove = playersToMove;
    }

    public sealed class PlayersToMoveChangedEventDto(List<int> playersToMove)
    {
        public List<int> PlayersToMove = playersToMove;
    }

    public sealed class GameWonEventDto(int playerId, Dictionary<int, int> playerScoresToIds)
    {
        public int PlayerId = playerId;
        public Dictionary<int, int> PlayerScoresToIds = playerScoresToIds;
    }
}
