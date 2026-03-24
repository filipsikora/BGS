using Catan.Shared.Data;
using System.Collections.Generic;

namespace Catan.Core.Snapshots
{
    public sealed class ResourcesAvailabilitySnapshot
    {
        public Dictionary<EnumResourceType, bool> ResourcesAvailability;
        public ResourcesAvailabilitySnapshot(Dictionary<EnumResourceType, bool> resourcesAvailability)
        {
            ResourcesAvailability = resourcesAvailability;
        }
    }
}
