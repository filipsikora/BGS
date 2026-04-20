using Catan.Core.DomainEvents;
using Catan.Core.Results;
using Catan.Core.Rules;
using Catan.Shared.Data;

namespace Catan.Core.PhaseLogic
{
    public sealed class BuildFreeRoadLogic : BaseLogic
    {
        public BuildFreeRoadLogic(GameSession session) : base(session) { }

        public ResultBuildFreeRoad Handle(int edgeId)
        {
            var player = Session.GetCurrentPlayer();
            var edge = Session.GetEdgeById(edgeId);
            var validation = RulesBuilding.CanBuildFreeRoad(player, edge, Session);

            if (!validation.Success)
            {
                return ResultBuildFreeRoad.Fail(validation.Reason, player.ID, edgeId);
            }

            Session.RoadBuiltMutation(edge);
            Session.RoadBuildingContextMutation();

            EnumGamePhases? nextPhase = Session.GetRoadsLeftToBuild() == 0 ? EnumGamePhases.NormalRound : null;

            var result = ResultBuildFreeRoad.Ok(player.ID, edgeId, nextPhase);
            result.AddDomainEvent(new RoadPlacedEvent(edgeId, result.PlayerId)).AddDomainEvent(new PlayerStateChangedEvent(result.PlayerId));

            return result;
        }
    }
}