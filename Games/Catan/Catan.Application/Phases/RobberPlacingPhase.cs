using Catan.Application.Controllers;
using Catan.Application.UIMessages;
using Catan.Application.Commands;
using Catan.Shared.Data;

namespace Catan.Application.Phases
{
    public class RobberPlacingPhase : BasePhase
    {
        private bool _clickableHexes = true;

        public RobberPlacingPhase(Facade facade) : base(facade) { }

        public override GameResult Handle(object command, int playerId)
        {
            switch (command)
            {
                case HexClickedCommand c:
                    return HandleHexClicked(c, playerId);

                case VictimChosenCommand c:
                    return VictimChosen(c, playerId);

                default:
                    return GameResult.Fail();
            }
        }

        private GameResult HandleHexClicked(HexClickedCommand signal, int playerId)
        {
            if (!_clickableHexes)
                return GameResult.Fail();

            var hexId = signal.HexId;
            var result = Facade.UseBlockHex(hexId);

            if (!result.Success)
                return GameResult.Fail().AddUIMessage(new ActionRejectedMessage(Facade.GetCurrentPlayerId(), result.Reason));


            var gameResult = HandleVictimsAfterBlocking(result.CanSteal, result.PotentialVictimsIds);

            _clickableHexes = false;

            return gameResult.AddDomainEventsList(result.DomainEvents);
        }

        private GameResult HandleVictimsAfterBlocking(bool canSteal, List<int>? potentialVictimsIds)
        {
            if (!canSteal)
            {
                return GameResult.Ok(EnumGamePhases.NormalRound);
            }

            else
            {
                return GameResult.Ok().AddUIMessage(new PotentialVictimsFoundMessage(potentialVictimsIds));
            }
        }

        private GameResult VictimChosen(VictimChosenCommand signal, int playerId)
        {
            var result = Facade.UseSelectVictim(signal.VictimId);
            
            if (!result.Success)
            {
                return GameResult.Fail().AddUIMessage(new ActionRejectedMessage(playerId, result.Reason));
            }

            return GameResult.Ok(result.NextPhase);
        }
    }
}