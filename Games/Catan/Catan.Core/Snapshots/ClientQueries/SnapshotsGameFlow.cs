using Catan.Shared.Data;

namespace Catan.Core.Snapshots.ClientQueries
{
    public sealed class FullGameFlowSnapshot
    {
        public int TurnNumber;
        public int RolledNumber;
        public int? CurrentPlayerId;
        public List<int> PlayersToMove;

        public int? KnightChampionId;
        public int? RoadChampionId;

        public EnumGamePhases CurrentPhase;
        public Dictionary<EnumResourceType, int> Resources;

        public FullGameFlowSnapshot(int turnNumber, int rolledNumber, int? currentPlayerId, int? knightChampionId, int? roadChampionId, EnumGamePhases currentPhase, 
            Dictionary<EnumResourceType, int> resources, List<int> playersToMove)
        {
            TurnNumber = turnNumber;
            RolledNumber = rolledNumber;
            CurrentPlayerId = currentPlayerId;
            KnightChampionId = knightChampionId;
            RoadChampionId = roadChampionId;
            CurrentPhase = currentPhase;
            Resources = resources;
            PlayersToMove = playersToMove;
        }
    }

    public sealed class TurnDataSnapshot
    {
        public int PlayerId { get; }
        public string PlayerName { get; }
        public int TurnNumber { get; }
        public int RolledNumber { get; }
        public bool InitialRoundsRemaining { get; }

        public TurnDataSnapshot(int playerdId, string playerName, int turnNumber, int rolledNumber, bool initialRoundsRemaining)
        {
            PlayerId = playerdId;
            PlayerName = playerName;
            TurnNumber = turnNumber;
            RolledNumber = rolledNumber;
            InitialRoundsRemaining = initialRoundsRemaining;
        }
    }
}