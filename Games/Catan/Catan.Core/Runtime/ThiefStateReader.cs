using Catan.Core.Models;
using Catan.Core.Rules;

namespace Catan.Core.Runtime
{
    public sealed class ThiefStateReader
    {
        public ThiefStateReader() { }

        public bool GetPlayersLeftToDiscard(List<Player> playerList)
        {
            return playerList.Any(p => p.Resources.Total() > 7);
        }

        public List<int> GetCardsDiscardingPlayers(List<Player> playerList)
        {
            return playerList.Where(p => p.Resources.Total() > 7).Select(p => p.ID).ToList();
        }

        public bool CanPlayerDiscard(ResourceCostOrStock resourcesSelected, Player discardingPlayer)
        {
            var result = RulesCardDiscard.CanDiscard(discardingPlayer, resourcesSelected);

            return result.Success;
        }

        public List<int> GetPossibleVictimsIds(IEnumerable<Player> possibleVictims, Player currentPlayer)
        {
            return possibleVictims.Where(player => player != currentPlayer && player.Resources.Total() > 0).Select(player => player.ID).ToList();
        }
    }
}
