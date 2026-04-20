using Catan.Core.DomainEvents;
using Catan.Core.Results;
using Catan.Core.Rules;

namespace Catan.Core.PhaseLogic
{
    public sealed class UpgradeVillageLogic : BaseLogic
    {
        public UpgradeVillageLogic(GameSession session) : base(session) { }

        public  ResultUpgradeVillage Handle(int vertexId)
        {
            var vertex = Session.GetVertexById(vertexId);
            var player = Session.GetCurrentPlayer();
            var validation = RulesBuilding.CanUpgradeVillage(player, vertex, Session);

            if (!validation.Success)
            {
                return ResultUpgradeVillage.Fail(validation.Reason, player.ID, vertexId);
            }

            Session.TownPaidAndBuiltMutation(vertex);

            var result = ResultUpgradeVillage.Ok(player.ID, vertexId, null);
            result.AddDomainEvent(new TownPlacedEvent(vertexId, result.PlayerId)).AddDomainEvent(new PlayerStateChangedEvent(result.PlayerId));

            return result;
        }
    }
}