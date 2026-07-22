using Catan.Application.Controllers;
using Catan.Application.UIMessages;
using Catan.Application.Commands;
using Catan.Shared.Data;

namespace Catan.Application.Phases
{
    public class DevelopmentCardsPhase : BasePhase
    {
        public DevelopmentCardsPhase(Facade facade) : base(facade) { }

        public override GameResult Handle(object command, int playerId)
        {
            switch (command)
            {
                case DevelopmentCardClickedPlayedCommand c:
                    return HandlePlayDevCard(c, playerId);

                case DevelopmentCardsCanceledCommand c:
                    return GameResult.Ok(Facade.GetNextPhaseFromAfterRoll());

                default:
                    return GameResult.Fail();
            }
        }

        private GameResult HandlePlayDevCard(DevelopmentCardClickedPlayedCommand signal, int playerId)
        {
            var result = Facade.UseDevCard(signal.DevelopmentCardId, playerId);

            if (!result.Success)
            {
                if (result.Reason == ConditionFailureReason.NoBuildingsAvailable)
                    return GameResult.Fail().AddUIMessage(new ActionRejectedMessage(playerId, result.Reason));

                if (result.Reason == ConditionFailureReason.NotEnoughResourcesInBank)
                    return GameResult.Fail().AddUIMessage(new ActionRejectedMessage(playerId, result.Reason));

                return GameResult.Fail().AddUIMessage(new ActionRejectedMessage(playerId, result.Reason));
            }

            return GameResult.Ok(result.NextPhase).AddDomainEventsList(result.DomainEvents);
        }
    }
}