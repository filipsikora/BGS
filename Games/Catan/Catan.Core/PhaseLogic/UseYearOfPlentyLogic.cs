using Catan.Core.DomainEvents;
using Catan.Core.Models;
using Catan.Core.Results;
using Catan.Core.Rules;
using Catan.Shared.Data;

namespace Catan.Core.PhaseLogic
{
    public sealed class UseYearOfPlentyLogic : BaseLogic
    {
        public UseYearOfPlentyLogic(GameSession session) : base(session) { }

        public ResultYearOfPlenty Handle(ResourceCostOrStock requested)
        {
            var validation = RulesDevCards.YearOfPlentyPlayedRight(Session.GetBank(), requested);

            if (!validation.Success)
            {
                return ResultYearOfPlenty.Fail(validation.Reason);
            }

            Session.UseYearOfPlentyMutation(requested);

            var result = ResultYearOfPlenty.Ok(requested, EnumGamePhases.NormalRound);
            result.AddDomainEvent(new PlayerStateChangedEvent(Session.GetCurrentPlayerId()));

            return result;
        }
    }
}