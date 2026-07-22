using Catan.Application.Interfaces;
using Catan.Core.Models;
using Catan.Shared.Data;

namespace Catan.Application.Commands
{
    public class OfferTradeCommand : ICommand { }

    public class TradeOfferCanceledCommand : ICommand { }

    public class TradePartnerChosenCommand(int buyerId, Dictionary<EnumResourceType, int> resources) : ICommand
    {
        public int PlayerId = buyerId;
        public ResourceCostOrStock Resources = ResourceCostOrStock.FromDictionary(resources);
    }

    public class TradeRequestAcceptedCommand : ICommand { }

    public class RefuseTradeRequestCommand : ICommand { }

    public class RequestTradeDataCommand : ICommand { }

    public class AcceptTradeRequestCommand : ICommand { }
}