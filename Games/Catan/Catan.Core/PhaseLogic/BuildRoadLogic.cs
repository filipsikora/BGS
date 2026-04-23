using Catan.Core.DomainEvents;
using Catan.Core.Results;
using Catan.Core.Rules;

namespace Catan.Core.PhaseLogic
{
    public sealed class BuildRoadLogic : BaseLogic
    {
        public BuildRoadLogic(GameSession session) : base(session) { }

        public ResultBuildRoad Handle(int edgeId)
        {
            var player = Session.GetCurrentPlayer();

            var validation = RulesBuilding.CanBuildRoad(player, edgeId, Session);

            if (!validation.Success)
            {
                return ResultBuildRoad.Fail(validation.Reason, player.ID, edgeId);
            }

            var edge = Session.GetEdgeById(edgeId);

            Session.RoadPaidAndBuiltMutation(edge);

            var result = ResultBuildRoad.Ok(player.ID, edgeId, null);
            result.AddDomainEvent(new RoadPlacedEvent(edgeId, result.PlayerId)).AddDomainEvent(new PlayerStateChangedEvent(result.PlayerId));

            return result;
        }
    }
}