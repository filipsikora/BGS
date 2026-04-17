using Catan.Core.Snapshots;
using Catan.Shared.Data;
using Catan.Shared.Dtos;

namespace Catan.Backend.Mappers
{
    public static class QueryMappers
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
                PlayerResources = snapshot.PlayerResources.ToDictionary(
                    kvp => kvp.Key.ToString(),
                    kvp => kvp.Value)
            };
        }

        public static ResourcesAvailabilityDto MapResourcesAvailabilityToDto(ResourcesAvailabilitySnapshot snapshot)
        {
            return new ResourcesAvailabilityDto
            {
                ResourcesAvailability = snapshot.ResourcesAvailability.ToDictionary(
                    kvp => kvp.Key.ToString(),
                    kvp => kvp.Value)
            };
        }

        public static IReadOnlyList<DevelopmentCardDto> MapCurrentPlayerDevCardsToDto(IReadOnlyList<DevelopmentCardSnapshot> snapshot)
        {
            return snapshot.Select(devCard => new DevelopmentCardDto
            {
                Id = devCard.Id,
                IsNew = devCard.IsNew,
                IsPlayable = devCard.IsPlayable,
                Type = devCard.Type.ToString()
            }).ToList();
        }

        public static IReadOnlyList<PlayerNameDto> MapNotCurrentPlayerNamesToDto(IReadOnlyList<PlayerNameSnapshot> snapshot)
        {
            return snapshot.Select(playerName => new PlayerNameDto
            {
                Id = playerName.Id,
                Name = playerName.Name
            }).ToList();
        }

        public static IReadOnlyList<PlayerNameDto> MapSomePlayersNamesToDto(IReadOnlyList<PlayerNameSnapshot> snapshot)
        {
            return snapshot.Select(playerName => new PlayerNameDto
            {
                Id = playerName.Id,
                Name = playerName.Name
            }).ToList();
        }

        public static TradeOfferedDto MapTradeOfferToDto(TradeOfferedSnapshot snapshot)
        {
            return new TradeOfferedDto
            {
                SellerId = snapshot.SellerId,
                BuyerId = snapshot.BuyerId,
                SellerName = snapshot.SellerName,
                BuyerName = snapshot.BuyerName,
                Offered = snapshot.Offered.ToDictionary(
                    kvp => kvp.Key.ToString(),
                    kvp => kvp.Value),
                Desired = snapshot.Desired.ToDictionary(
                    kvp => kvp.Key.ToString(),
                    kvp => kvp.Value),
                CanTrade = snapshot.CanTrade
            };
        }

        public static EnumQueryName MapStringToEnum(string queryName)
        {
            return queryName switch
            {
                "board" => EnumQueryName.Board,
                "current-player-dev-cards" => EnumQueryName.CurrentPlayerDevCards,
                "not-current-player-names" => EnumQueryName.NotCurrentPlayerNames,
                "player-cards" => EnumQueryName.PlayerCards,
                "player-data" => EnumQueryName.PlayerData,
                "resources-availability" => EnumQueryName.ResourcesAvailability,
                "trade-offer-data" => EnumQueryName.TradeOfferData,
                "victim-cards" => EnumQueryName.VictimCards,
                _ => throw new Exception($"Unknown query name: {queryName}")
            };
        }
    }
}