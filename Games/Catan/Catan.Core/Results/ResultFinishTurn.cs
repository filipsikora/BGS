using Catan.Shared.Data;

namespace Catan.Core.Results
{
    public sealed class ResultFinishTurn : ResultBase
    {
        public int NewCurrentPlayerId { get; }
        public bool InitialRoundsRemaining { get; }
        public int NewTurnNumber { get; }

        private ResultFinishTurn(int newCurrentPlayerId, bool initialRoundsRemaining, int newTurnNumber, EnumGamePhases nextPhase) : base(true, nextPhase)
        {
            NewCurrentPlayerId = newCurrentPlayerId;
            InitialRoundsRemaining = initialRoundsRemaining;
            NewTurnNumber = newTurnNumber;
        }

        public static ResultFinishTurn Ok(int newCurrentPlayerId, bool initialRoundsRemaining, int newTurnNumber, EnumGamePhases nextPhase)
        {
            return new ResultFinishTurn(newCurrentPlayerId, initialRoundsRemaining, newTurnNumber, nextPhase);
        }
    }
}
