using Catan.Core.DomainEvents;
using Catan.Core.Results;
using Catan.Core.Rules;

namespace Catan.Core.PhaseLogic
{
    public sealed class BuildInitialVillageLogic : BaseLogic
    {
        public BuildInitialVillageLogic(GameSession session) : base(session) { }

        public ResultBuildInitialVillage Handle(int vertexId)
        {
            var player = Session.GetCurrentPlayer();
            var vertex = Session.GetVertexById(vertexId);

            var validation = RulesBuilding.CanBuildInitialVillage(player, vertex, Session);

            if (!validation.Success)
            {
                return ResultBuildInitialVillage.Fail(validation.Reason, player.ID, vertexId);
            }

            var secondVillage = player.Points == 1;

            Session.VillageBuiltMutation(vertex, secondVillage);

            var result = ResultBuildInitialVillage.Ok(player.ID, vertexId, null);
            result.AddDomainEvent(new VillagePlacedEvent(vertexId, result.PlayerId)).AddDomainEvent(new PlayerStateChangedEvent(result.PlayerId));

            return result;
        }
    }
}