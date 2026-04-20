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

        public override GameResult Handle(object command)
        {
            switch (command)
            {
                case BankTradeOfferedResourceSelectedCommand c:
                    return HandleOfferedResourceSelected(c);

                case BankTradeCanceledCommand c:
                    return GameResult.Ok(EnumGamePhases.NormalRound);

                case BankTradeDesiredResourceSelectedCommand c:
                    return HandleBankTrade(c);

                default:
                    return GameResult.Fail();
            }
        }

        private GameResult HandleOfferedResourceSelected(BankTradeOfferedResourceSelectedCommand signal)
        {
            _offered = signal.Type;
            var ratio = Facade.GetCurrentPlayerTradeRatio(signal.Type);

            int amount = Facade.GetCurrentPlayerResourceAmount(signal.Type);
            bool possibleForPlayer = Facade.PlayerHasEnoughResources(amount, ratio);

            return GameResult.Ok().AddUIMessage(new BankTradeRatioChangedMessage(ratio, possibleForPlayer, _offered));
        }

        private GameResult HandleBankTrade(BankTradeDesiredResourceSelectedCommand signal)
        {
            var desired = signal.Type;

            if (_offered == null || desired == null)
                return GameResult.Fail();

            var result = Facade.UseBankTrade(_offered.Value, desired.Value);
            
            if (!result.Success)
            {
                return GameResult.Fail().AddUIMessage(new ActionRejectedMessage(result.PlayerId, result.Reason));
            }

            return GameResult.Ok(result.NextPhase).AddUIMessage(new LogMessageMessage(EnumLogTypes.Info, $"player{result.PlayerId} trade {result.Ratio} {result.Offered} for 1 {result.Desired}")).
                AddDomainEventsList(result.DomainEvents);
        }
    }
}