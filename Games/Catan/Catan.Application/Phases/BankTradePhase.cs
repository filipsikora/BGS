using Catan.Application.Controllers;
using Catan.Application.UIMessages;
using Catan.Application.Commands;
using Catan.Shared.Data;

namespace Catan.Application.Phases
{
    public sealed class BankTradePhase : BasePhase
    {
        private EnumResourceType? _offered;

        public BankTradePhase(Facade facade) : base(facade) { }

        public override GameResult Handle(object command, int playerId)
        {
            switch (command)
            {
                case BankTradeOfferedResourceSelectedCommand c:
                    return HandleOfferedResourceSelected(c, playerId);

                case BankTradeCanceledCommand c:
                    return GameResult.Ok(EnumGamePhases.NormalRound);

                case BankTradeDesiredResourceSelectedCommand c:
                    return HandleBankTrade(c, playerId);

                default:
                    return GameResult.Fail();
            }
        }

        private GameResult HandleOfferedResourceSelected(BankTradeOfferedResourceSelectedCommand signal, int playerId)
        {
            _offered = signal.Type;
            var ratio = Facade.GetPlayerTradeRatioById(signal.Type, playerId);

            int amount = Facade.GetPlayerResourceAmountById(signal.Type, playerId);
            bool possibleForPlayer = Facade.PlayerHasEnoughResources(amount, ratio);

            return GameResult.Ok().AddUIMessage(new BankTradeRatioChangedMessage(ratio, possibleForPlayer, _offered));
        }

        private GameResult HandleBankTrade(BankTradeDesiredResourceSelectedCommand signal, int playerId)
        {
            var desired = signal.Type;

            if (_offered == null || desired == null)
                return GameResult.Fail();

            var result = Facade.UseBankTrade(_offered.Value, desired.Value, playerId);
            
            if (!result.Success)
            {
                return GameResult.Fail().AddUIMessage(new ActionRejectedMessage(result.PlayerId, result.Reason));
            }

            return GameResult.Ok(result.NextPhase).AddDomainEventsList(result.DomainEvents);
        }
    }
}