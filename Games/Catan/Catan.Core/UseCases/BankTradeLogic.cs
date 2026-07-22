using Catan.Core.DomainEvents;
using Catan.Core.Results;
using Catan.Core.Rules;
using Catan.Core.Runtime;
using Catan.Shared.Data;

namespace Catan.Core.UseCases
{
    public sealed class BankTradeLogic : BaseUseCase
    {
        public BankTradeLogic(GameSession session) : base(session) { }

        public ResultBankTrade Handle(EnumResourceType offered, EnumResourceType desired, int playerId)
        {
            var player = Session.GetPlayerById(playerId);
            var ratio = Session.GetPlayerTradeRatioById(offered, playerId);
            var bank = Session.GetBank();
            
            var validation = RulesTrade.CanTradeWithBank(player, bank, offered, desired, ratio);
            
            if (!validation.Success)
            {
                return ResultBankTrade.Fail(player.ID, validation.Reason);
            }

            Session.BankTradeMutation(offered, desired, ratio, playerId);

            var result = ResultBankTrade.Ok(player.ID, offered, desired, ratio, EnumGamePhases.NormalRound);
            result.AddDomainEvent(new BankTradeDoneEvent(player.ID, offered, desired, ratio, Session.GetBank().ToDictionary(), player.Resources.ToDictionary()));

            return ApplyPhase(result);
        }
    }
}