using Catan.Core.DomainEvents;
using Catan.Core.Results;
using Catan.Core.Rules;
using Catan.Core.Runtime;
using Catan.Shared.Data;

namespace Catan.Core.UseCases
{
    public sealed class BuildInitialRoadLogic : BaseUseCase
    {
        public BuildInitialRoadLogic(GameSession session) : base(session) { }

        public ResultBuildInitialRoad Handle(int edgeId, int vertexId, int playerId)
        {
            var player = Session.GetPlayerById(playerId);

            var validation = RulesBuilding.CanBuildInitialRoad(player, edgeId, vertexId, Session);

            if (!validation.Success)
            {
                return ResultBuildInitialRoad.Fail(validation.Reason, player.ID, edgeId);
            }

            var edge = Session.GetEdgeById(edgeId);

            Session.RoadBuiltMutation(edge, player);

            var result = ResultBuildInitialRoad.Ok(player.ID, edgeId, null);
            result.AddDomainEvent(new RoadPlacedEvent(edgeId, result.PlayerId, player.BuildingsLeftCount(EnumBuildings.Road), player.Resources.ToDictionary()));

            return ApplyPhase(result);
        }
    }
}