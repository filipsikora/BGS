using Catan.Core.Models;
using Catan.Shared.Data;

namespace Catan.Core.Runtime
{
    public sealed class TradeStateReader
    {
        public TradeStateReader() { }

        public int GetCurrentPlayerTradeRatio(EnumResourceType resource, Player currentPlayer, Port rightPort)
        {
            if (currentPlayer.Ports.Count != 0)
            {
                bool hasThreeToOnePort = currentPlayer.Ports.Any(port => port.Type == null);

                if (currentPlayer.Ports.Contains(rightPort))
                    return 2;

                if (hasThreeToOnePort)
                    return 3;
            }

            return 4;
        }
    }
}