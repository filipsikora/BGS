using Catan.Core.Snapshots.ClientQueries;

namespace Catan.Core.Queries.Interfaces
{
    public interface IResourcesQueryService
    {
        ResourcesAvailabilitySnapshot GetResourcesAvailability();
    }
}