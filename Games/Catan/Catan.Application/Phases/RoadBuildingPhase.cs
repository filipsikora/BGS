using Catan.Application.Controllers;
using Catan.Application.UIMessages;
using Catan.Application.Commands;

namespace Catan.Application.Phases
{
    public class RoadBuildingPhase : BasePhase
    {
        public RoadBuildingPhase(Facade facade) : base(facade) { }

        public override GameResult Handle(object command, int playerId)
        {
            switch (command)
            {
                case EdgeClickedCommand c:
                    return HandleEdgeClicked(c);

                case BuildRoadCommand c:
                    return HandleRoadRequested(c, playerId);

                default:
                    return GameResult.Fail();
            }
        }

        private GameResult HandleEdgeClicked(EdgeClickedCommand signal)
        {
            var (village, road, town) = Facade.GetEdgeBuildOptions(signal.EdgeId);

            return GameResult.Ok().AddUIMessage(new EdgeHighlightedMessage(signal.EdgeId)).AddUIMessage(new BuildOptionsSentMessage(village, road, town));
        }

        private GameResult HandleRoadRequested(BuildRoadCommand signal, int playerId)
        {
            var result = Facade.UseBuildFreeRoad(signal.EdgeId, playerId);

            if (!result.Success)
            {
                return GameResult.Fail().AddUIMessage(new ActionRejectedMessage(playerId, result.Reason));
            }

            return GameResult.Ok(result.NextPhase).AddDomainEventsList(result.DomainEvents);
        }
    }
}