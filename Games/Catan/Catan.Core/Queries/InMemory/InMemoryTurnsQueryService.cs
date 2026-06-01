using Catan.Core.Queries.Interfaces;
using Catan.Core.Runtime;
using Catan.Core.Snapshots.ClientQueries;

namespace Catan.Core.Queries.InMemory
{
    public sealed class InMemoryTurnsQueryService : ITurnsQueryService
    {
        private readonly GameSession _session;

        public InMemoryTurnsQueryService(GameSession session)
        {
            _session = session;
        }

        public TurnDataSnapshot GetTurnData()
        {
            var currentPlayer = _session.GetCurrentPlayer();
            var turnNumber = _session.GetTurn();
            var rolledNumber = _session.GetLastRoll();
            var initialRoundsRemaining = _session.CheckIfInitialRoundsRemaining();

            return new TurnDataSnapshot(currentPlayer.ID, currentPlayer.Name, turnNumber, rolledNumber, initialRoundsRemaining);
        }
    }
}