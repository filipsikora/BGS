using Catan.Core.DomainEvents;
using Catan.Core.Results;
using Catan.Core.Runtime;
using Catan.Shared.Data;

namespace Catan.Core.UseCases
{
    public sealed class RollDiceLogic : BaseUseCase
    {
        public RollDiceLogic(GameSession session) : base(session) { }

        public ResultRollDice Handle()
        {
            Session.RollDice();

            var rolledSevenButNoVictims = false;
            var resultDistributionList = Session.ServePlayersMutation();
            var resultRoll = Session.DiceRolledMutation();
            var nextPhase = EnumGamePhases.NormalRound;
            ResultRollDice result;

            if (resultRoll == 7)
            {
                if (!Session.GetPlayersLeftToDiscard())
                {
                    nextPhase = EnumGamePhases.RobberPlacing;
                    rolledSevenButNoVictims = true;
                }

                else
                {
                    nextPhase = EnumGamePhases.CardDiscarding;
                }
            }

            result = ResultRollDice.Ok(resultRoll, resultDistributionList, nextPhase, rolledSevenButNoVictims);

            var playersIdsToResourceChange = resultDistributionList.GroupBy(x => x.PlayerId).ToDictionary(
                x => x.Key,
                x => x.GroupBy(y => y.Type).ToDictionary(
                    y => y.Key,
                    y => y.Sum(z => z.Granted))
            );

            var playersIdsToResources = resultDistributionList.Select(x => x.PlayerId).Distinct().ToDictionary(
                x => x,
                x => Session.GetPlayerCardsById(x).ToDictionary()
            );

            result.AddDomainEvent(new ResourcesDistributionDoneEvent(playersIdsToResources, playersIdsToResourceChange, Session.GetBank().ToDictionary()));

            result.AddDomainEvent(new RolledNumberChangedEvent(resultRoll)).AddDomainEvent(new DevCardPlayabilityChangedEvent(Session.GetCurrentPlayerId(), Session.GetCurrentPlayerDevCardsData().Select(d => d.Id)));

            return ApplyPhase(result);
        }
    }
}