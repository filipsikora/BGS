#nullable enable
using Catan.Application.Controllers;
using Catan.Application.UIMessages;
using Catan.Shared.Data;

namespace Catan.Application.Phases
{
    public abstract class BasePhase
    {
        protected readonly Facade Facade;

        protected BasePhase(Facade facade)
        {
            Facade = facade;
        }

        public abstract GameResult Handle(object command, int playerId);

        public virtual void Enter()
        {
            Facade.SetPlayersToMove([Facade.GetCurrentPlayerId()]);
        }

        public virtual GameResult ValidatePlayer(int playerId)
        {
            if (!Facade.GetPlayersToMove().Contains(playerId))
                return GameResult.Fail().AddUIMessage(new ActionRejectedMessage(playerId, ConditionFailureReason.NotCurrentPlayer));

            return GameResult.Ok();
        }
    }
}