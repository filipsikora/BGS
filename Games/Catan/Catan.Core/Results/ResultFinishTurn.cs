using Catan.Shared.Data;

namespace Catan.Core.Results
{
    public sealed class ResultFinishTurn : ResultBase
    {
        public ConditionFailureReason Reason { get; set; }
        public int NewCurrentPlayerId { get; }
        public bool InitialRoundsRemaining { get; }
        public int NewTurnNumber { get; }

        private ResultFinishTurn(bool success, ConditionFailureReason reason, int newCurrentPlayerId, bool initialRoundsRemaining, int newTurnNumber, EnumGamePhases? nextPhase) : base(true, nextPhase)
        {
            NewCurrentPlayerId = newCurrentPlayerId;
            InitialRoundsRemaining = initialRoundsRemaining;
            NewTurnNumber = newTurnNumber;
        }

        public static ResultFinishTurn Ok(int newCurrentPlayerId, bool initialRoundsRemaining, int newTurnNumber, EnumGamePhases nextPhase)
        {
            return new ResultFinishTurn(true, ConditionFailureReason.None, newCurrentPlayerId, initialRoundsRemaining, newTurnNumber, nextPhase);
        }

        public static ResultFinishTurn Fail(ConditionFailureReason reason, int currentPlayerId, bool initialRoundsRemaining, int turnNumber)
        { 
            return new ResultFinishTurn(false, reason, currentPlayerId, initialRoundsRemaining, turnNumber, null);
        }
    }
}
