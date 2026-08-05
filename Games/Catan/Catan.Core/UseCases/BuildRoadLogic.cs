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

            var roadChampionResult = Session.RoadPaidAndBuiltMutation(edge, playerId);

            var result = ResultBuildRoad.Ok(player.ID, edgeId, null);

            if (roadChampionResult.Changed)
                result.AddDomainEvent(new RoadChampionChangedEvent(roadChampionResult.OldChampion?.ID, roadChampionResult.NewChampion?.ID, roadChampionResult.OldChampion?.ExtraPoints,
                    roadChampionResult.NewChampion?.ExtraPoints, roadChampionResult.OldChampion?.Points, roadChampionResult.NewChampion?.Points));


            result.AddDomainEvent(new RoadPlacedEvent(edgeId, playerId, player.BuildingsLeftCount(EnumBuildings.Road), player.Resources.ToDictionary(), Session.GetBank().ToDictionary()));

            return ApplyPhase(result);
        }
    }
}