using Catan.Shared.Interfaces;

namespace Catan.Shared.Dtos.UIMessages
{
    public sealed class DevelopmentCardBoughtMessageDto : IUiMessageDto
    {
        public int CardId;
        public DevelopmentCardBoughtMessageDto(int cardId)
        {
            CardId = cardId;
        }
    }
}
