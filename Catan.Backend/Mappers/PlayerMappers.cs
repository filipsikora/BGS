using Catan.Core.Snapshots;
using Catan.Shared.Dtos;

namespace Catan.Backend.Mappers
{
    public static class PlayerMappers
    {
        public static PlayerDataDto MapPlayerDataToDto(PlayerDataSnapshot snapshot)
        {
            return new PlayerDataDto
            {
                Name = snapshot.Name,
                BuildingsLeft = snapshot.BuildingsLeft,
                Points = snapshot.Points,
                Knights = snapshot.Knights,
                VictoryPoints = snapshot.VictoryPoints,
                ExtraPoints = snapshot.ExtraPoints
            };
        }

        public static PlayerCardsDto MapPlayerCardsToDto(PlayerResourcesSnapshot snapshot)
        {
            return new PlayerCardsDto
            {
                PlayerResources = snapshot.PlayerResources
            };
        }
    }
}