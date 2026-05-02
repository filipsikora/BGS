using Catan.Core.DomainEvents;
using Catan.Core.Results;
using Catan.Core.Rules;

namespace Catan.Core.UseCases
{
    public sealed class BuildVillageLogic : BaseUseCase
    {
        public BuildVillageLogic(GameSession session) : base(session) { }

        public ResultBuildVillage Handle(int vertexId)
        {
            var player = Session.GetCurrentPlayer();

            var validation = RulesBuilding.CanBuildVillage(player, vertexId, Session);

            if (!validation.Success)
            {
                return ResultBuildVillage.Fail(validation.Reason, player.ID, vertexId);
            }

            var vertex = Session.GetVertexById(vertexId);

            Session.VillagePaidAndBuiltMutation(vertex);

            var result = ResultBuildVillage.Ok(player.ID, vertexId, null);
            result.AddDomainEvent(new VillagePlacedEvent(vertexId, result.PlayerId)).AddDomainEvent(new PlayerStateChangedEvent(result.PlayerId));

            return ApplyPhase(result);
        }
    }
}