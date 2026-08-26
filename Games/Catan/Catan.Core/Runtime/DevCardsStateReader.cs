using Catan.Core.Models;
using Catan.Core.Snapshots.ClientQueries;
using Catan.Shared.Data;

namespace Catan.Core.Runtime
{
    public sealed class DevCardsStateReader
    {
        public DevCardsStateReader() { }

        public IReadOnlyList<DevelopmentCardSnapshot> GetCurrentPlayerDevCardsData(IReadOnlyList<DevelopmentCard> currentPlayerDevCards, bool afterRoll)
        {
            return currentPlayerDevCards.Select(card => Map(card, afterRoll)).ToList();
        }

        public List<DevelopmentCardSnapshot> GetPlayerDevCardsByIdData(List<DevelopmentCard> playerDevCards, bool afterRoll)
        {
            return playerDevCards.Select(card => Map(card, afterRoll)).ToList();
        }

        public List<int> GetPlayersKnightCardsIds(IReadOnlyList<DevelopmentCard> playersDevCards)
        {
            return playersDevCards.Where(d => d.Type == EnumDevelopmentCardTypes.Knight).Select(d => d.ID).ToList();
        }

        private DevelopmentCardSnapshot Map(DevelopmentCard card, bool afterRoll)
        {
            bool isPlayable = !card.IsNew && (afterRoll || card.Type == EnumDevelopmentCardTypes.Knight);

            return new DevelopmentCardSnapshot(card.ID, card.Type, card.IsNew, isPlayable);
        }
    }
}