using Catan.Core.Snapshots.ClientQueries;
using Catan.Shared.Data;
using Catan.Shared.Dtos;

namespace Catan.Backend.Mappers
{
    public static class QueryMappers
    {
        public static TradeOfferedDto MapTradeOfferToDto(TradeOfferedSnapshot snapshot)
        {
            return new TradeOfferedDto
            {
                SellerId = snapshot.SellerId,
                BuyerId = snapshot.BuyerId,
                SellerName = snapshot.SellerName,
                BuyerName = snapshot.BuyerName,
                Offered = snapshot.Offered,
                Desired = snapshot.Desired,
                CanTrade = snapshot.CanTrade
            };
        }

        public static EnumQueryName MapStringToEnum(string queryName)
        {
            return queryName switch
            {
                "trade-offer-data" => EnumQueryName.TradeOfferData,
                "victim-cards" => EnumQueryName.VictimCards,
                _ => throw new Exception($"Unknown query name: {queryName}")
            };
        }
    }
}