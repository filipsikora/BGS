using Catan.Core.DomainEvents;
using Catan.Core.Results;
using Catan.Core.Rules;

namespace Catan.Core.UseCases
{
    public sealed class BuildInitialVillageLogic : BaseUseCase
    {
        public BuildInitialVillageLogic(GameSession session) : base(session) { }

        public ResultBuildInitialVillage Handle(int vertexId)
        {
            var player = Session.GetCurrentPlayer();

            var validation = RulesBuilding.CanBuildInitialVillage(player, vertexId, Session);

            if (!validation.Success)
            {
                return ResultBuildInitialVillage.Fail(validation.Reason, player.ID, vertexId);
            }

            var secondVillage = player.Points == 1;
            var vertex = Session.GetVertexById(vertexId);

            Session.VillageBuiltMutation(vertex, secondVillage);

            var result = ResultBuildInitialVillage.Ok(player.ID, vertexId, null);
            result.AddDomainEvent(new VillagePlacedEvent(vertexId, result.PlayerId)).AddDomainEvent(new PlayerStateChangedEvent(result.PlayerId));

            return ApplyPhase(result);
        }
    }
}