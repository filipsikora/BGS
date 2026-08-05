using Catan.Core.DomainEvents;
using Catan.Core.Results;
using Catan.Core.Rules;
using Catan.Core.Runtime;
using Catan.Shared.Data;

namespace Catan.Core.UseCases
{
    public sealed class BuildFreeRoadLogic : BaseUseCase
    {
        public BuildFreeRoadLogic(GameSession session) : base(session) { }

        public ResultBuildFreeRoad Handle(int edgeId, int playerId)
        {
            var player = Session.GetPlayerById(playerId);
            var validation = RulesBuilding.CanBuildFreeRoad(player, edgeId, Session);

            if (!validation.Success)
            {
                return ResultBuildFreeRoad.Fail(validation.Reason, player.ID, edgeId);
            }

            var edge = Session.GetEdgeById(edgeId);

            var roadChampionResult = Session.RoadBuiltMutation(edge, player);
            Session.RoadBuildingContextMutation();

            EnumGamePhases? nextPhase = Session.GetRoadsLeftToBuild() ? null : EnumGamePhases.NormalRound;

            var result = ResultBuildFreeRoad.Ok(player.ID, edgeId, nextPhase);

            if (roadChampionResult.Changed)
                result.AddDomainEvent(new RoadChampionChangedEvent(roadChampionResult.OldChampion?.ID, roadChampionResult.NewChampion?.ID, roadChampionResult.OldChampion?.ExtraPoints,
                    roadChampionResult.NewChampion?.ExtraPoints, roadChampionResult.OldChampion?.Points, roadChampionResult.NewChampion?.Points));

            result.AddDomainEvent(new RoadPlacedEvent(edgeId, result.PlayerId, player.BuildingsLeftCount(EnumBuildings.Road), player.Resources.ToDictionary(), Session.GetBank().ToDictionary()));

            return ApplyPhase(result);
        }
    }
}