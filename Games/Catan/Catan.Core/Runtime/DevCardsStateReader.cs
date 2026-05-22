using Catan.Core.Models;
using Catan.Core.Snapshots.ClientQueries;
using Catan.Shared.Data;

namespace Catan.Core.Runtime
{
    public sealed class DevCardsStateReader
    {
        public DevCardsStateReader() { }

        public IReadOnlyList<DevelopmentCardSnapshot> GetCurrentPlayerDevCards(IReadOnlyList<DevelopmentCard> currentPlayerDevCardw, bool afterRoll)
        {
            return currentPlayerDevCardw.Select(card => Map(card, afterRoll)).ToList();
        }

        public List<DevelopmentCardSnapshot> GetPlayerDevCardsById(List<DevelopmentCard> playerDevCards, bool afterRoll)
        {
            return playerDevCards.Select(card => Map(card, afterRoll)).ToList();
        }

        private DevelopmentCardSnapshot Map(DevelopmentCard card, bool afterRoll)
        {
            bool isPlayable = !card.IsNew && (afterRoll || card.Type == EnumDevelopmentCardTypes.Knight);

            return new DevelopmentCardSnapshot(card.ID, card.Type, card.IsNew, isPlayable);
        }
    }
}
}