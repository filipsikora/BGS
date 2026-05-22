using Catan.Core.Snapshots.ClientQueries;
using System.Collections.Generic;

namespace Catan.Core.Queries.Interfaces
{
    public interface IDevCardsQueryService
    {
        IReadOnlyList<DevelopmentCardSnapshot> GetCurrentPlayerDevCards();
    }
}