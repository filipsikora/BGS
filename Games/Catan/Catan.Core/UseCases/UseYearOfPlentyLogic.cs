using Catan.Core.DomainEvents;
using Catan.Core.Models;
using Catan.Core.Results;
using Catan.Core.Rules;
using Catan.Core.Runtime;
using Catan.Shared.Data;

namespace Catan.Core.UseCases
{
    public sealed class UseYearOfPlentyLogic : BaseUseCase
    {
        public UseYearOfPlentyLogic(GameSession session) : base(session) { }

        public ResultYearOfPlenty Handle(ResourceCostOrStock requested, int playerId)
        {
            var validation = RulesDevCards.YearOfPlentyPlayedRight(Session.GetBank(), requested);

            if (!validation.Success)
            {
                return ResultYearOfPlenty.Fail(validation.Reason);
            }

            Session.UseYearOfPlentyMutation(requested, playerId);

            var player = Session.GetPlayerById(playerId);
            var result = ResultYearOfPlenty.Ok(requested, EnumGamePhases.NormalRound);

            result.AddDomainEvent(new PlayerResourcesReceivedEvent(playerId, requested.ToDictionary(), player.Resources.ToDictionary(), Session.GetBank().ToDictionary()));

            return ApplyPhase(result);
        }
    }
}