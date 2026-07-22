using Catan.Application.Controllers;
using Catan.Application.Interfaces;
using Catan.Application.UIMessages;
using Catan.Application.Commands;
using Catan.Core.DomainEvents;

namespace Catan.Application.Phases
{
    public class CardDiscardingPhase : BasePhase
    {
        public CardDiscardingPhase(Facade facade) : base(facade) { }

        public override void Enter()
        {
            var playersToMove = Facade.GetPlayersToDiscard();
            Facade.SetPlayersToMove(playersToMove);
        }

        public override GameResult Handle(object command, int playerId)
        {
            switch (command)
            {
                case CardsSelectedCommand c:
                    return HandleCardsDiscarded(c, playerId);

                default:
                    return GameResult.Fail();
            }
        }

        private GameResult HandleCardsDiscarded(CardsSelectedCommand signal, int playerId)
        {
            var resources = signal.Resources;
            var result = Facade.UseDiscard(playerId, resources);

            if (!result.Success)
            {
                return GameResult.Fail().AddUIMessage(new ActionRejectedMessage(playerId, result.Reason));
            }

            Facade.RemovePlayerFromToMove(playerId);

            result.AddDomainEvent(new CardsDiscardedEvent(playerId, resources.ToDictionary(), Facade.GetPlayerCardsById(playerId), Facade.GetBank()));

            if (result.NextPhase == null)
                result.AddDomainEvent(new PlayersToMoveChangedEvent(Facade.GetPlayersToMove()));

            return GameResult.Ok(result.NextPhase);
        }
    }
}