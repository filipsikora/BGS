using Catan.Core.DomainEvents;
using Catan.Core.Results;
using Catan.Core.Rules;
using Catan.Core.Runtime;
using Catan.Shared.Data;

namespace Catan.Core.UseCases
{
    public sealed class BuildVillageLogic : BaseUseCase
    {
        public BuildVillageLogic(GameSession session) : base(session) { }

        public ResultBuildVillage Handle(int vertexId, int playerId)
        {
            var player = Session.GetPlayerById(playerId);

            var validation = RulesBuilding.CanBuildVillage(player, vertexId, Session);

            if (!validation.Success)
            {
                return ResultBuildVillage.Fail(validation.Reason, player.ID, vertexId);
            }

            var vertex = Session.GetVertexById(vertexId);

            var roadChampionResult = Session.VillagePaidAndBuiltMutation(vertex, playerId);

            var result = ResultBuildVillage.Ok(player.ID, vertexId, null);

            if (roadChampionResult.Changed)
                result.AddDomainEvent(new RoadChampionChangedEvent(roadChampionResult.OldChampionId, roadChampionResult.NewChampionId));

            result.AddDomainEvent(new VillagePlacedEvent(vertexId, playerId, player.Points, player.BuildingsLeftCount(EnumBuildings.Village), player.Resources.ToDictionary()));

            return ApplyPhase(result);
        }
    }
}