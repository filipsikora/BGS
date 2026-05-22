using Catan.Core.Queries.Interfaces;
using Catan.Core.Runtime;
using Catan.Core.Snapshots.ClientQueries;

namespace Catan.Core.Queries.InMemory
{
    public sealed class InMemoryDevCardQueryService : IDevCardsQueryService
    {
        private readonly GameSession _session;

        public InMemoryDevCardQueryService(GameSession session)
        {
            _session = session;
        }

        public IReadOnlyList<DevelopmentCardSnapshot> GetCurrentPlayerDevCards() => _session.GetCurrentPlayerDevCards();

        public IReadOnlyList<DevelopmentCardSnapshot> GetPlayerDevCardsById(int playerId) => _session.GetPlayerDevCardsById(playerId);
    }
}
