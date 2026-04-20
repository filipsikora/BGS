using Catan.Application.Interfaces;
namespace Catan.Application.UIMessages
{
    public sealed class DevelopmentCardBoughtMessage : IUIMessages
    {
        public int CardId;
        public DevelopmentCardBoughtMessage(int cardId)
        {
            CardId = cardId;
        }
    }
}