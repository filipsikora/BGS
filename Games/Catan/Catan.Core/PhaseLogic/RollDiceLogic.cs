using Catan.Core.DomainEvents;
using Catan.Core.Results;
using Catan.Shared.Data;

namespace Catan.Core.PhaseLogic
{
    public sealed class RollDiceLogic : BaseLogic
    {
        public RollDiceLogic(GameSession session) : base(session) { }

        public ResultRollDice Handle()
        {
            Session.RollDice();

            var rolledSevenButNoVictims = false;
            var resultDistributionList = Session.ServePlayersMutation();
            var resultRoll = Session.DiceRolledMutation();
            var nextPhase = EnumGamePhases.NormalRound;

            if (resultRoll == 7)
            {
                if (!Session.GetPlayersLeftToDiscard())
                {
                    nextPhase = EnumGamePhases.RobberPlacing;
                    rolledSevenButNoVictims = true;
                }

                else
                {
                    Session.GetPlayersToDiscard();
                    nextPhase = EnumGamePhases.CardDiscarding;
                }
            }

            var result = ResultRollDice.Ok(resultRoll, resultDistributionList, nextPhase, rolledSevenButNoVictims);
            result.AddDomainEvent(new RolledNumberChangedEvent(resultRoll));

            return result;
        }
    }
}