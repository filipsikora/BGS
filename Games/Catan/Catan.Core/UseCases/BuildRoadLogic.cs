using Catan.Core.DomainEvents;
using Catan.Core.Results;
using Catan.Core.Rules;
using Catan.Core.Runtime;
using Catan.Shared.Data;

namespace Catan.Core.UseCases
{
    public sealed class BuildRoadLogic : BaseUseCase
    {
        public BuildRoadLogic(GameSession session) : base(session) { }

        public ResultBuildRoad Handle(int edgeId, int playerId)
        {
            var player = Session.GetPlayerById(playerId);

            var validation = RulesBuilding.CanBuildRoad(player, edgeId, Session);

            if (!validation.Success)
            {
                return ResultBuildRoad.Fail(validation.Reason, player.ID, edgeId);
            }

            var edge = Session.GetEdgeById(edgeId);

            var roadChampionUpdateResult = Session.RoadPaidAndBuiltMutation(edge, playerId);

            var result = ResultBuildRoad.Ok(player.ID, edgeId, null);

            if (roadChampionUpdateResult.Changed)
            {

                result.AddDomainEvent(new RoadChampionChangedEvent(roadChampionUpdateResult.OldChampion?.ID, roadChampionUpdateResult.NewChampion?.ID, roadChampionUpdateResult.OldChampion?.ExtraPoints, 
                    roadChampionUpdateResult.NewChampion?.ExtraPoints, roadChampionUpdateResult.OldChampion?.Points, roadChampionUpdateResult.NewChampion?.Points);
            }

            result.AddDomainEvent(new RoadPlacedEvent(edgeId, playerId, player.BuildingsLeftCount(EnumBuildings.Road), player.Resources.ToDictionary()));

            return ApplyPhase(result);
        }
    }
}