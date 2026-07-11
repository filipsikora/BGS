using Catan.Core.Models;
using Catan.Shared.Data;

namespace Catan.Core.Runtime
{
    public sealed class TradeStateReader
    {
        public TradeStateReader() { }

        public int GetPlayerTradeRatioById(EnumResourceType resource, Player player, Port rightPort)
        {
            if (player.Ports.Count != 0)
            {
                bool hasThreeToOnePort = player.Ports.Any(port => port.Type == null);

                if (player.Ports.Contains(rightPort))
                    return 2;

                if (hasThreeToOnePort)
                    return 3;
            }

            return 4;
        }
    }
}