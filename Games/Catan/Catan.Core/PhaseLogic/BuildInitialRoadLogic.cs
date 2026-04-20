using Catan.Core.DomainEvents;
using Catan.Core.Results;
using Catan.Core.Rules;

namespace Catan.Core.PhaseLogic
{
    public sealed class BuildInitialRoadLogic : BaseLogic
    {
        public BuildInitialRoadLogic(GameSession session) : base(session) { }

        public ResultBuildInitialRoad Handle(int edgeId, int vertexId)
        {
            var player = Session.GetCurrentPlayer();
            var edge = Session.GetEdgeById(edgeId);
            var vertex = Session.GetVertexById(vertexId);

            var validation = RulesBuilding.CanBuildInitialRoad(player, edge, vertex, Session);

            if (!validation.Success)
            {
                return ResultBuildInitialRoad.Fail(validation.Reason, player.ID, edgeId);
            }

            Session.RoadBuiltMutation(edge);

            var result = ResultBuildInitialRoad.Ok(player.ID, edgeId, null);
            result.AddDomainEvent(new RoadPlacedEvent(edgeId, result.PlayerId)).AddDomainEvent(new PlayerStateChangedEvent(result.PlayerId));

            return result;
        }
    }
}