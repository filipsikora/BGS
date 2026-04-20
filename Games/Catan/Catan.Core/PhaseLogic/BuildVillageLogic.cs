using Catan.Core.DomainEvents;
using Catan.Core.Results;
using Catan.Core.Rules;

namespace Catan.Core.PhaseLogic
{
    public sealed class BuildVillageLogic : BaseLogic
    {
        public BuildVillageLogic(GameSession session) : base(session) { }

        public ResultBuildVillage Handle(int vertexId)
        {
            var player = Session.GetCurrentPlayer();
            var vertex = Session.GetVertexById(vertexId);

            var validation = RulesBuilding.CanBuildVillage(player, vertex, Session);

            if (!validation.Success)
            {
                return ResultBuildVillage.Fail(validation.Reason, player.ID, vertexId);
            }

            Session.VillagePaidAndBuiltMutation(vertex);

            var result = ResultBuildVillage.Ok(player.ID, vertexId, null);
            result.AddDomainEvent(new VillagePlacedEvent(vertexId, result.PlayerId)).AddDomainEvent(new PlayerStateChangedEvent(result.PlayerId));

            return result;
        }
    }
}