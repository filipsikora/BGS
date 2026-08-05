using Catan.Core.DomainEvents;
using Catan.Core.Results;
using Catan.Core.Rules;
using Catan.Core.Runtime;
using Catan.Shared.Data;

namespace Catan.Core.UseCases
{
    public sealed class BuildInitialVillageLogic : BaseUseCase
    {
        public BuildInitialVillageLogic(GameSession session) : base(session) { }

        public ResultBuildInitialVillage Handle(int vertexId, int playerId)
        {
            var player = Session.GetPlayerById(playerId);

            var validation = RulesBuilding.CanBuildInitialVillage(player, vertexId, Session);

            if (!validation.Success)
            {
                return ResultBuildInitialVillage.Fail(validation.Reason, player.ID, vertexId);
            }

            var secondVillage = player.Points == 1;
            var vertex = Session.GetVertexById(vertexId);

            Session.VillageBuiltMutation(vertex, secondVillage, player);

            var result = ResultBuildInitialVillage.Ok(player.ID, vertexId, null);

            result.AddDomainEvent(new VillagePlacedEvent(vertexId, result.PlayerId, player.Points, player.BuildingsLeftCount(EnumBuildings.Village), player.Resources.ToDictionary(),
                Session.GetBank().ToDictionary()));

            return ApplyPhase(result);
        }
    }
}