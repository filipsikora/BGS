using Catan.Core.DomainEvents;
using Catan.Core.Results;
using Catan.Core.Rules;
using Catan.Core.Runtime;

namespace Catan.Core.UseCases
{
    public sealed class UpgradeVillageLogic : BaseUseCase
    {
        public UpgradeVillageLogic(GameSession session) : base(session) { }

        public  ResultUpgradeVillage Handle(int vertexId, int playerId)
        {
            var player = Session.GetPlayerById(playerId);
            var validation = RulesBuilding.CanUpgradeVillage(player, vertexId, Session);

            if (!validation.Success)
            {
                return ResultUpgradeVillage.Fail(validation.Reason, player.ID, vertexId);
            }

            var vertex = Session.GetVertexById(vertexId);

            Session.TownPaidAndBuiltMutation(vertex, playerId);

            var result = ResultUpgradeVillage.Ok(player.ID, vertexId, null);
            result.AddDomainEvent(new TownPlacedEvent(vertexId, result.PlayerId)).AddDomainEvent(new PlayerStateChangedEvent(result.PlayerId));

            return ApplyPhase(result);
        }
    }
}