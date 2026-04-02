using Catan.Application.Interfaces;

namespace Catan.Application.UIMessages
{
    public sealed class PlayerSelectedToDiscardMessage : IUIMessages
    {
        public int PlayerId;
        public PlayerSelectedToDiscardMessage(int playerId)
        {
            PlayerId = playerId;
        }
    }

    public sealed class PotentialVictimsFoundMessage : IUIMessages
    {
        public List<int> VictimsIds { get; }

        public PotentialVictimsFoundMessage(List<int> victimsIds)
        {
            VictimsIds = victimsIds;
        }
    }
}
