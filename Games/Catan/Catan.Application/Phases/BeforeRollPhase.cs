using Catan.Application.Controllers;
using Catan.Application.UIMessages;
using Catan.Application.Commands;
using Catan.Shared.Data;

namespace Catan.Application.Phases
{
    public class BeforeRollPhase : BasePhase
    {
        public BeforeRollPhase(Facade facade) : base(facade) { }
        
        public override GameResult Handle(object command, int playerId)
        {
            switch (command)
            {
                case RollDiceCommand c:
                    return HandleRollDice(c, playerId);

                case ShowDevelopmentCardsCommand c:
                    return GameResult.Ok(EnumGamePhases.DevelopmentCards);

                case VertexClickedCommand:
                case EdgeClickedCommand:
                case HexClickedCommand:
                    return HandleInvalidClick(playerId);

                default:
                    return GameResult.Fail();
            }
        }

        private GameResult HandleRollDice(RollDiceCommand signal, int playerId)
        {
            var result = Facade.UseRollDice();

            return GameResult.Ok(result.NextPhase.Value).AddDomainEventsList(result.DomainEvents);
        }

        private GameResult HandleInvalidClick(int playerId)
        {
            return GameResult.Ok().AddUIMessage(new ActionRejectedMessage(playerId, ConditionFailureReason.NotRolledYet));
        }
    }
}