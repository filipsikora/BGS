using Catan.Core.DomainEvents;
using Catan.Core.Results;
using Catan.Core.Rules;
using Catan.Shared.Data;

namespace Catan.Core.UseCases
{
    public sealed class BuildFreeRoadLogic : BaseUseCase
    {
        public BuildFreeRoadLogic(GameSession session) : base(session) { }

        public ResultBuildFreeRoad Handle(int edgeId)
        {
            var player = Session.GetCurrentPlayer();
            var validation = RulesBuilding.CanBuildFreeRoad(player, edgeId, Session);

            if (!validation.Success)
            {
                return ResultBuildFreeRoad.Fail(validation.Reason, player.ID, edgeId);
            }

            var edge = Session.GetEdgeById(edgeId);

            Session.RoadBuiltMutation(edge);
            Session.RoadBuildingContextMutation();

            EnumGamePhases? nextPhase = Session.GetRoadsLeftToBuild() ? null : EnumGamePhases.NormalRound;

            var result = ResultBuildFreeRoad.Ok(player.ID, edgeId, nextPhase);
            result.AddDomainEvent(new RoadPlacedEvent(edgeId, result.PlayerId)).AddDomainEvent(new PlayerStateChangedEvent(result.PlayerId));

            return ApplyPhase(result);
        }
    }
}