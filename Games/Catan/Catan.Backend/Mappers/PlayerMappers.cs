using Catan.Core.Snapshots.ClientQueries;
using Catan.Shared.Dtos;

namespace Catan.Backend.Mappers
{
    public static class PlayerMappers
    {
        public static FullPlayerDto MapFullPlayerToDto(FullPlayerDataSnapshot dataSnapshot, PlayerResourcesSnapshot resourcesSnapshot)
        {
            return new FullPlayerDto
            {
                Data = MapFullPlayerDataToDto(dataSnapshot),
                Resources = MapPlayerCardsToDto(resourcesSnapshot)
            };
        }

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

        public static FullPlayerDataDto MapFullPlayerDataToDto(FullPlayerDataSnapshot snapshot)
        {
            return new FullPlayerDataDto
            {
                Name = snapshot.Name,
                PlayerId = snapshot.PlayerId,
                BuildingsLeft = snapshot.BuildingsLeft,
                Points = snapshot.Points,
                Knights = snapshot.Knights,
                VictoryPoints = snapshot.VictoryPoints,
                ExtraPoints = snapshot.ExtraPoints,
                DevCards = snapshot.DevCards.Select(devCard => new DevelopmentCardDto
                {
                    Id = devCard.Id,
                    IsNew = devCard.IsNew,
                    IsPlayable = devCard.IsPlayable,
                    Type = devCard.Type
                }).ToList()
            };
        }

        public static BasicPlayerDto MapBsaicPlayerDataToDto(BasicPlayerSnapshot snapshot)
        {
            return new BasicPlayerDto
            {
                Id = snapshot.Id,
                Name = snapshot.Name,
                ResourceCardsNumber = snapshot.ResourceCardsNumber,
                DevCardsNumber = snapshot.DevCardsNumber,
                VictoryCardsPlayed = snapshot.VictoryCardsPlayed,
                KnightCardsPlayed = snapshot.KnightCardsPlayed
            };
        }

        public static OtherPlayersDto MapOtherPlayersDataToDto(OtherPlayersSnapshot snapshot)
        {
            return new OtherPlayersDto
            {
                OtherPlayers = snapshot.OtherPlayers.Select(MapBsaicPlayerDataToDto).ToList()
            };
        }

        public static PlayerResourcesDto MapPlayerCardsToDto(PlayerResourcesSnapshot snapshot)
        {
            return new PlayerResourcesDto
            {
                PlayerResources = snapshot.PlayerResources.ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value)
            };
        }
    }
}
