using Catan.Core.Queries.Interfaces;
using Catan.Shared.Data;
using Catan.Core.Runtime;
using Catan.Core.Snapshots.ClientQueries;

namespace Catan.Core.Queries.InMemory
{
    public sealed class InMemoryResourcesQueryService : IResourcesQueryService
    {
        private readonly GameSession _session;

        public InMemoryResourcesQueryService(GameSession session)
        {
            _session = session;
        }

        public ResourcesAvailabilitySnapshot GetResourcesAvailability() => _session.GetResourcesAvailabilityData();
    }
}