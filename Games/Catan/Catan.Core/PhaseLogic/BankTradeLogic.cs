using Catan.Core.DomainEvents;
using Catan.Core.Results;
using Catan.Core.Rules;
using Catan.Shared.Data;

namespace Catan.Core.PhaseLogic
{
    public sealed class BankTradeLogic : BaseLogic
    {
        public BankTradeLogic(GameSession session) : base(session) { }

        public ResultBankTrade Handle(EnumResourceType offered, EnumResourceType desired)
        {
            var player = Session.GetCurrentPlayer();
            var ratio = Session.GetCurrentPlayerTradeRatio(offered);
            
            var validation = RulesTrade.CanTradeWithBank(player, Session.GetBank(), offered, desired, ratio);
            
            if (!validation.Success)
            {
                return ResultBankTrade.Fail(player.ID, validation.Reason);
            }

            Session.BankTradeMutation(offered, desired, ratio);

            var result = ResultBankTrade.Ok(player.ID, offered, desired, ratio, EnumGamePhases.NormalRound);
            result.AddDomainEvent(new PlayerStateChangedEvent(player.ID));

            return result;
        }
    }
}