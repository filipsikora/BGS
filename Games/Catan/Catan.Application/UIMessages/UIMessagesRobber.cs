using Catan.Application.Interfaces;

namespace Catan.Application.UIMessages
{
    public sealed class PotentialVictimsFoundMessage : IUIMessages
    {
        public List<int> VictimsIds { get; }

        public PotentialVictimsFoundMessage(List<int> victimsIds)
        {
            VictimsIds = victimsIds;
        }
    }
}
