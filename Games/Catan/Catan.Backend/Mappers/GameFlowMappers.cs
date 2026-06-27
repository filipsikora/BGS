using Catan.Core.Snapshots.ClientQueries;
using Catan.Shared.Dtos;

namespace Catan.Backend.Mappers
{
    public static class GameFlowMappers
    {
        public static FullGameFlowDto MapFullGameFlowToDto(FullGameFlowSnapshot snapshot)
        {
            return new FullGameFlowDto
            {
                CurrentPhase = snapshot.CurrentPhase,
                CurrentPlayerId = snapshot.CurrentPlayerId,
                KnightChampionId = snapshot.KnightChampionId,
                RoadChampionId = snapshot.RoadChampionId,
                RolledNumber = snapshot.RolledNumber,
                TurnNumber = snapshot.TurnNumber
            };
        }
    }
}
