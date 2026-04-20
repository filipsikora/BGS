using Catan.Core.Conditions;
using Catan.Core.DomainEvents;
using Catan.Core.Results;
using Catan.Shared.Data;

namespace Catan.Core.PhaseLogic
{
    public sealed class UseMonopolyLogic : BaseLogic
    {
        public UseMonopolyLogic(GameSession session) : base(session) { }

        public ResultMonopolyCard Handle(EnumResourceType resource)
        {
            var player = Session.GetCurrentPlayer();

            var validation = ConditionsResources.ResourceExists(resource);

            if (!validation.Success)
            {
                return ResultMonopolyCard.Fail(validation.Reason, player.ID, resource);
            }

            var victimsIdsAndAmounts = Session.UseMonopolyMutation(resource);

            var result = ResultMonopolyCard.Ok(player.ID, victimsIdsAndAmounts, resource, EnumGamePhases.NormalRound);
            result.AddDomainEvent(new PlayerStateChangedEvent(result.ThiefId));

            return result;
        }
    }
}