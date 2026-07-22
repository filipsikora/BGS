using Catan.Application.Commands;
using Catan.Application.Controllers;
using Catan.Shared.Data;
using Catan.Application.UIMessages;

namespace Catan.Application.Phases
{
    public class TradeOfferPhase : BasePhase
    {
        public TradeOfferPhase(Facade facade) : base(facade) { }

        public override GameResult Handle(object command, int playerId)
        {
            switch (command)
            {
                case TradeOfferCanceledCommand c:
                    return GameResult.Ok(EnumGamePhases.NormalRound);

                case TradePartnerChosenCommand c:
                    return HandleTradePartnerChosen(c, playerId);

                default:
                    return GameResult.Fail();
            }
        }

        private GameResult HandleTradePartnerChosen(TradePartnerChosenCommand signal, int playerId)
        {
            var buyerId = signal.PlayerId;
            var result = Facade.UseOfferTrade(buyerId, signal.Resources, playerId);

            if (!result.Success)
            {
                return GameResult.Fail().AddUIMessage(new ActionRejectedMessage(result.SellerId, result.Reason));
            }

            return GameResult.Ok(result.NextPhase);
        }
    }
}