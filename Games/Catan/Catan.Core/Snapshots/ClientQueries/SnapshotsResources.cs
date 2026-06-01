using Catan.Shared.Data;
using System.Collections.Generic;

namespace Catan.Core.Snapshots.ClientQueries
{
    public sealed class FullBankSnapshot
    {
        public Dictionary<EnumResourceType, int> BankState;
        public List<DevelopmentCardSnapshot> DevCards;

        public FullBankSnapshot(Dictionary<EnumResourceType, int> bankState, List<DevelopmentCardSnapshot> devCards)
        {
            BankState = bankState;
            DevCards = devCards;
        }
    }

    public sealed class ResourcesAvailabilitySnapshot
    {
        public Dictionary<EnumResourceType, bool> ResourcesAvailability;
        public ResourcesAvailabilitySnapshot(Dictionary<EnumResourceType, bool> resourcesAvailability)
        {
            ResourcesAvailability = resourcesAvailability;
        }
    }
}
