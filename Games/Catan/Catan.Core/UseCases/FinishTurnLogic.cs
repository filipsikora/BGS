using Catan.Core.DomainEvents;
using Catan.Core.Results;
using Catan.Core.Rules;
using Catan.Core.Runtime;
using Catan.Shared.Data;

namespace Catan.Core.UseCases
{
    public sealed class FinishTurnLogic : BaseUseCase
    {
        public FinishTurnLogic(GameSession session) : base(session) { }

        public ResultFinishTurn Handle(int playerId)
        {
            var player = Session.GetPlayerById(playerId);
            var initialRound = Session.CheckIfIsCorePhase(EnumGamePhases.FirstRoundsBuilding);

            var validation = RulesTurn.CanFinishInitialTurn(Session, initialRound);
            Console.WriteLine($"initialRound: {initialRound}, result: {validation.Success}");

            if (initialRound && !validation.Success)
            {
                return ResultFinishTurn.Fail(ConditionFailureReason.InitialRoundNotFinished, playerId, true, Session.GetTurn());
            }

            Session.MarkDevCardsAsOldMutation(playerId);
            
            (int nextIndex, bool initialRoundsRemaining) = Session.GetNextIndex();

            Session.AdvanceToNextPlayerMutation(nextIndex);

            var nextTurnNumber = Session.GetTurn();
            var nextPhase = initialRoundsRemaining ? EnumGamePhases.FirstRoundsBuilding : EnumGamePhases.BeforeRoll;
            var result = ResultFinishTurn.Ok(player.ID, initialRoundsRemaining, nextTurnNumber, nextPhase);

            if (Session.WinCheck(playerId))
            {
                var gameScore = Session.GameWon(playerId);

                result.AddDomainEvent(new GameWonEvent(gameScore.WinnderId, gameScore.PlayerScoresToIds));
            }

            return ApplyPhase(result);
        }
    }
}