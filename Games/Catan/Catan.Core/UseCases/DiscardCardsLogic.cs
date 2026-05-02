using Catan.Core.Models;
using Catan.Core.Results;
using Catan.Core.Rules;
using Catan.Shared.Data;

namespace Catan.Core.UseCases
{
    public sealed class DiscardCardsLogic : BaseUseCase
    {
        public DiscardCardsLogic(GameSession session) : base(session) { }

        public ResultCondition Handle(int playerId, ResourceCostOrStock selectedCards)
        {
            var player = Session.GetPlayerById(playerId);
            var result = RulesCardDiscard.CanDiscard(player, selectedCards);

            if (!result.Success)
            {
                return result;
            }

            Session.CardsDiscardedMutation(player, selectedCards);
            Session.CardsDiscardedContextMutation();

            EnumGamePhases? nextPhase = Session.GetCardDiscardingContextExistance() ? null : EnumGamePhases.RobberPlacing;

            return ApplyPhase(ResultCondition.Ok(nextPhase));
        }
    }
}