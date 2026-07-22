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

            foreach (var distribution in resultDistributionList)
            {
                var resourcesChange = new Dictionary<EnumResourceType, int>
                {
                    { distribution.Type, distribution.Granted }
                };

                result.AddDomainEvent(new PlayerResourcesReceivedEvent(distribution.PlayerId, resourcesChange, Session.GetPlayerCardsById(distribution.PlayerId).ToDictionary(), Session.GetBank().ToDictionary()));
            }

            result.AddDomainEvent(new RolledNumberChangedEvent(resultRoll));

            return ApplyPhase(result);
        }
    }
}