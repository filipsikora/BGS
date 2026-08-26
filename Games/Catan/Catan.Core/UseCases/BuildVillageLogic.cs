using Catan.Core.DomainEvents;
using Catan.Core.Results;
using Catan.Core.Rules;
using Catan.Core.Runtime;

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
                result.AddDomainEvent(new RoadChampionChangedEvent(roadChampionResult.OldChampion?.ID, roadChampionResult.NewChampion?.ID, roadChampionResult.OldChampion?.ExtraPoints, 
                    roadChampionResult.NewChampion?.ExtraPoints, roadChampionResult.OldChampion?.Points, roadChampionResult.NewChampion?.Points));

            result.AddDomainEvent(new VillagePlacedEvent(vertexId, playerId, player.Points, player.Resources.ToDictionary(), Session.GetBank().ToDictionary(), player.GetBuildingsLeft()));

            return ApplyPhase(result);
        }
    }
}