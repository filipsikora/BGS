using Catan.Core.Conditions;
using Catan.Core.DomainEvents;
using Catan.Core.Results;
using Catan.Core.Runtime;
using Catan.Shared.Data;

namespace Catan.Core.UseCases
{
    public sealed class UseMonopolyLogic : BaseUseCase
    {
        public UseMonopolyLogic(GameSession session) : base(session) { }

        public ResultMonopolyCard Handle(EnumResourceType resource, int playerId)
        {
            var player = Session.GetPlayerById(playerId);

            var validation = ConditionsResources.ResourceExists(resource);

            if (!validation.Success)
            {
                return ResultMonopolyCard.Fail(validation.Reason, player.ID, resource);
            }

            var victimsIdsAndAmounts = Session.UseMonopolyMutation(resource, player);

            var result = ResultMonopolyCard.Ok(player.ID, victimsIdsAndAmounts, resource, EnumGamePhases.NormalRound);

            foreach (var idToAmount in victimsIdsAndAmounts)
            {
                var id = idToAmount.Key;

                result.AddDomainEvent(new CardsStolenEvent(resource, idToAmount.Value, player.ID, id, player.Resources.ToDictionary(), Session.GetPlayerById(id).Resources.ToDictionary()));
            }

            return ApplyPhase(result);
        }
    }
}