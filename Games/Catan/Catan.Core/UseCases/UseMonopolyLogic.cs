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

            var playersIdsToResources = victimsIdsAndAmounts.ToDictionary(
                x => x.Key,
                x => Session.GetPlayerById(x.Key).Resources.ToDictionary());
            playersIdsToResources.Add(player.ID, player.Resources.ToDictionary());

            result.AddDomainEvent(new CardsStolenEvent(resource, playerId, victimsIdsAndAmounts, playersIdsToResources));

            return ApplyPhase(result);
        }
    }
}