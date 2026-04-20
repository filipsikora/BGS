using Catan.Shared.Interfaces;

namespace Catan.Shared.Dtos.UiMessages
{
    public sealed class PlayerSelectedToDiscardDto : IUiMessageDto
    {
        public int PlayerId;
        public PlayerSelectedToDiscardDto(int playerId)
        {
            PlayerId = playerId;
        }
    }

    public sealed class PotentialVictimsFoundDto : IUiMessageDto
    {
        public List<int> VictimsIds { get; }

        public PotentialVictimsFoundDto(List<int> victimsIds)
        {
            VictimsIds = victimsIds;
        }
    }

    public sealed class RobberPlacedMessageDto : IUiMessageDto
    {
        public int HexId { get; }
        public RobberPlacedMessageDto(int hexId)
        {
            HexId = hexId;
        }
    }
}
