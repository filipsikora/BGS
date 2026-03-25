using Catan.Application.Interfaces;

namespace Catan.Application.Commands
{
    public class OfferTradeCommand : ICommand { }

    public class TradeOfferCanceledCommand : ICommand { }

    public class TradePartnerChosenCommand : ICommand
    {
        public int PlayerId { get; }
        public TradePartnerChosenCommand(int playerId)
        {
            PlayerId = playerId;
        }
    }

    public class TradeRequestAcceptedCommand : ICommand { }

    public class RefuseTradeRequestCommand : ICommand { }

    public class RequestTradeDataCommand : ICommand { }

    public class AcceptTradeRequestCommand : ICommand { }
}