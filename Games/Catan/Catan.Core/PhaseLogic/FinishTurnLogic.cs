using Catan.Core.DomainEvents;
using Catan.Core.Results;
using Catan.Core.Rules;
using Catan.Shared.Data;

namespace Catan.Core.PhaseLogic
{
    public sealed class FinishTurnLogic : BaseLogic
    {
        public FinishTurnLogic(GameSession session) : base(session) { }

        public ResultFinishTurn Handle()
        {
            var player = Session.GetCurrentPlayer();
            var initialRound = Session.CheckIfIsInitialRound();

            var validation = RulesTurn.CanFinishInitialTurn(Session, initialRound);
            Console.WriteLine($"initialRound: {initialRound}, result: {validation.Success}");

            if (initialRound && !validation.Success)
            {
                return ResultFinishTurn.Fail(ConditionFailureReason.InitialRoundNotFinished, Session.GetCurrentPlayerId(), true, Session.GetTurn());
            }

            Session.MarkDevCardsAsOldMutation();
            
            (int nextIndex, bool initialRoundsRemaining) = Session.GetNextIndex();

            Session.AdvanceToNextPlayerMutation(nextIndex);
            Session.WinCheck();

            var nextTurnNumber = Session.GetTurn();
            var nextPhase = initialRoundsRemaining ? EnumGamePhases.FirstRoundsBuilding : EnumGamePhases.BeforeRoll;

            var result = ResultFinishTurn.Ok(player.ID, initialRoundsRemaining, nextTurnNumber, nextPhase);
            result.AddDomainEvent(new PlayerStateChangedEvent(nextIndex + 1)).AddDomainEvent(new TurnNumberChangedEvent(nextTurnNumber));

            return result;
        }
    }
}