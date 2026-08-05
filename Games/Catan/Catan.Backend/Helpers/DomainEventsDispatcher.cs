using BGS.Shared.Dtos;
using Catan.Backend.GameManagement;
using Catan.Core.DomainEvents;
using Catan.Core.Interfaces;
using Catan.Shared.Dtos.DomainEvents;
using Newtonsoft.Json.Linq;

namespace Catan.Backend.Helpers
{
    public class DomainEventsDispatcher
    {
        public List<GameUpdateDto> Dispatch(IDomainEvent domainEvent, CatanGameInstance game)
        {
            var type = domainEvent.Type.ToString();

            return domainEvent switch
            {
                BankTradeDoneEvent e => BroadcastToAll(e, game, type),
                RolledNumberChangedEvent e => BroadcastToAll(e, game, type),
                PlayerResourcesReceivedEvent e => DispatchPlayerResourcesReceivedEvent(e, game, type),
                PhaseChangedEvent e => BroadcastToAll(e, game, type),
                CardsDiscardedEvent e => DispatchCardsDiscardedEvent(e, game, type),
                PlayersToMoveChangedEvent e => BroadcastToAll(e, game, type),
                CardStolenEvent e => DispatchCardStolenEvent(e, game, type),
                DevCardUsedEvent e => BroadcastToAll(e, game, type),
                VillagePlacedEvent e => DispatchVillagePlacedEvent(e, game, type),
                RoadPlacedEvent e => DispatchRoadPlacedEvent(e, game, type),
                TownPlacedEvent e => DispatchTownPlacedEvent(e, game, type),
                GameWonEvent e => BroadcastToAll(e, game, type),
                CardsStolenEvent e => DispatchCardsStolenEvent(e, game, type),
                RoadChampionChangedEvent e => BroadcastToAll(e, game, type),
                DevCardBoughtEvent e => DispatchDevCardBoughtEvent(e, game, type),
                VictoryCardUsedEvent e => BroadcastToAll(e, game, type),
                KnightCardUsedEvent e => BroadcastToAll(e, game, type),
                TradeDoneEvent e => DispatchTradeDoneEvent(e, game, type),
                _ => throw new NotSupportedException($"Unknown domain event: {type}")
            };
        }            

        private List<GameUpdateDto> DispatchCardsDiscardedEvent(CardsDiscardedEvent domainEvent, CatanGameInstance game, string type)
        {
            var updatesList = new List<GameUpdateDto>(); 

            foreach (var entry in game.PlayerTokens)
            {
                if (entry.Value == domainEvent.PlayerId)
                    updatesList.Add(new GameUpdateDto(type, entry.Key, JToken.FromObject(new
                        CardsDiscardedEventPrivateDto(domainEvent.PlayerId, domainEvent.Resources, domainEvent.PlayerResources,
                        domainEvent.Bank))));

                else
                {
                    updatesList.Add(new GameUpdateDto(type, entry.Key, JToken.FromObject(new
                        CardsDiscardedPuvlicEventDto(domainEvent.PlayerId, domainEvent.Resources, domainEvent.PlayerResources.Values.Sum(), domainEvent.Bank))));
                }
            }

            return updatesList;
        }

        private List<GameUpdateDto> DispatchPlayerResourcesReceivedEvent(PlayerResourcesReceivedEvent domainEvent, CatanGameInstance game, string type)
        {
            var updatesList = new List<GameUpdateDto>();

            foreach (var entry in game.PlayerTokens)
            {
                if (entry.Value == domainEvent.PlayerId)
                    updatesList.Add(new GameUpdateDto(type, entry.Key, JToken.FromObject(new
                        PlayerResourcesReceivedEventPrivateDto(domainEvent.PlayerId, domainEvent.ResourcesChange, domainEvent.PlayerResources, domainEvent.Bank))));

                else
                {
                    updatesList.Add(new GameUpdateDto(type, entry.Key, JToken.FromObject(new
                        PlayerResourcesReceivedEventPublicDto(domainEvent.PlayerId, domainEvent.ResourcesChange, domainEvent.PlayerResources.Values.Sum(), domainEvent.Bank))));
                }
            }

            return updatesList;
        }

        private List<GameUpdateDto> DispatchCardStolenEvent(CardStolenEvent domainEvent, CatanGameInstance game, string type)
        {
            var updatesList = new List<GameUpdateDto>();

            foreach (var entry in game.PlayerTokens)
            {
                if (entry.Value == domainEvent.ThiefId)
                    updatesList.Add(new GameUpdateDto(type, entry.Key, JToken.FromObject(new
                    CardStolenEventThiefDto(domainEvent.Resource, domainEvent.ThiefId, domainEvent.VictimId, domainEvent.ThiefResources, domainEvent.VictimResources.Values.Sum()))));

                else if (entry.Value == domainEvent.VictimId)
                    updatesList.Add(new GameUpdateDto(type, entry.Key, JToken.FromObject(new
                        CardStolenEventVictimDto(domainEvent.Resource, domainEvent.ThiefId, domainEvent.VictimId, domainEvent.ThiefResources.Values.Sum(), domainEvent.VictimResources))));

                else
                {
                    updatesList.Add(new GameUpdateDto(type, entry.Key, JToken.FromObject(new
                        CardStolenEventPublicDto(domainEvent.Resource, domainEvent.ThiefId, domainEvent.VictimId, domainEvent.ThiefResources.Values.Sum(), domainEvent.VictimResources.Values.Sum()))));
                }
            }

            return updatesList;
        }

        private List<GameUpdateDto> DispatchVillagePlacedEvent(VillagePlacedEvent domainEvent, CatanGameInstance game, string type)
        {
            var updatesList = new List<GameUpdateDto>();

            foreach (var entry in game.PlayerTokens)
            {
                if (entry.Value == domainEvent.OwnerId)
                    updatesList.Add(new GameUpdateDto(type, entry.Key, JToken.FromObject(new
                        VillagePlacedEventPrivateDto(domainEvent.VertexId, domainEvent.OwnerId, domainEvent.Points, domainEvent.VillagesLeft, domainEvent.Resources, domainEvent.Bank.ToDictionary()))));

                else
                {
                    updatesList.Add(new GameUpdateDto(type, entry.Key, JToken.FromObject(new
                        VillagePlacedEventPublicDto(domainEvent.VertexId, domainEvent.OwnerId, domainEvent.Points, domainEvent.VillagesLeft, domainEvent.Resources.Values.Sum(), 
                        domainEvent.Bank.ToDictionary()))));
                }
            }

            return updatesList;
        }

        private List<GameUpdateDto> DispatchRoadPlacedEvent(RoadPlacedEvent domainEvent, CatanGameInstance game, string type)
        {
            var updatesList = new List<GameUpdateDto>();

            foreach (var entry in game.PlayerTokens)
            {
                if (entry.Value == domainEvent.OwnerId)
                    updatesList.Add(new GameUpdateDto(type, entry.Key, JToken.FromObject(new
                        RoadPlacedEventPrivateDto(domainEvent.EdgeId, domainEvent.OwnerId, domainEvent.RoadsLeft, domainEvent.Resources, domainEvent.Bank.ToDictionary()))));

                else
                {
                    updatesList.Add(new GameUpdateDto(type, entry.Key, JToken.FromObject(new
                        RoadPlacedEventPublicDto(domainEvent.EdgeId, domainEvent.OwnerId, domainEvent.RoadsLeft, domainEvent.Resources.Values.Sum(), domainEvent.Bank.ToDictionary()))));
                }
            }

            return updatesList;
        }

        private List<GameUpdateDto> DispatchTownPlacedEvent(TownPlacedEvent domainEvent, CatanGameInstance game, string type)
        {
            var updatesList = new List<GameUpdateDto>();

            foreach (var entry in game.PlayerTokens)
            {
                if (entry.Value == domainEvent.OwnerId)
                    updatesList.Add(new GameUpdateDto(type, entry.Key, JToken.FromObject(new
                        TownPlacedEventPrivateDto(domainEvent.VertexId, domainEvent.OwnerId, domainEvent.Points, domainEvent.TownsLeft, domainEvent.VillagesLeft, domainEvent.Resources, 
                        domainEvent.Bank.ToDictionary()))));

                else
                {
                    updatesList.Add(new GameUpdateDto(type, entry.Key, JToken.FromObject(new
                        TownPlacedEventPublicDto(domainEvent.VertexId, domainEvent.OwnerId, domainEvent.Points, domainEvent.TownsLeft, domainEvent.VillagesLeft, domainEvent.Resources.Values.Sum(), 
                        domainEvent.Bank.ToDictionary()))));
                }
            }

            return updatesList;
        }

        private List<GameUpdateDto> DispatchCardsStolenEvent(CardsStolenEvent domainEvent, CatanGameInstance game, string type)
        {
            var updatesList = new List<GameUpdateDto>();

            foreach (var entry in game.PlayerTokens)
            {
                if (entry.Value == domainEvent.ThiefId)
                {
                    updatesList.Add(new GameUpdateDto(type, entry.Key, JToken.FromObject(new
                        CardsStolenEventThiefDto(domainEvent.Resource, domainEvent.Quantity, domainEvent.ThiefId, domainEvent.VictimId, domainEvent.ThiefResources, domainEvent.VictimResources.Values.Sum()))));
                }

                else if (entry.Value == domainEvent.VictimId)
                {
                    updatesList.Add(new GameUpdateDto(type, entry.Key, JToken.FromObject(new
                        CardsStolenEventVictimDto(domainEvent.Resource, domainEvent.Quantity, domainEvent.ThiefId, domainEvent.VictimId, domainEvent.ThiefResources.Values.Sum(), domainEvent.VictimResources))));
                }

                else
                {
                    updatesList.Add(new GameUpdateDto(type, entry.Key, JToken.FromObject(new
                        CardsStolenEventPublicDto(domainEvent.Resource, domainEvent.Quantity, domainEvent.ThiefId, domainEvent.VictimId, domainEvent.ThiefResources.Values.Sum(), 
                        domainEvent.VictimResources.Values.Sum()))));
                }
            }

            return updatesList;
        }

        private List<GameUpdateDto> DispatchDevCardBoughtEvent(DevCardBoughtEvent domainEvent, CatanGameInstance game, string type)
        {
            var updatesList = new List<GameUpdateDto>();

            foreach (var entry in game.PlayerTokens)
            {
                if (entry.Value == domainEvent.PlayerId)
                {
                    updatesList.Add(new GameUpdateDto(type, entry.Key, JToken.FromObject(new
                        DevCardBoughtEventPrivateDto(domainEvent.PlayerId, domainEvent.CardId, domainEvent.DevCardType, domainEvent.DevCardNumber, domainEvent.Resources))));
                }

                else
                {
                    updatesList.Add(new GameUpdateDto(type, entry.Key, JToken.FromObject(new
                        DevCardBoughtEventPublicDto(domainEvent.PlayerId, domainEvent.DevCardNumber, domainEvent.Resources.Values.Sum()))));
                }
            }

            return updatesList;
        }

        private List<GameUpdateDto> DispatchTradeDoneEvent(TradeDoneEvent domainEvent, CatanGameInstance game, string type)
        {
            var updatesList = new List<GameUpdateDto>();

            foreach (var entry in game.PlayerTokens)
            {
                if (entry.Value == domainEvent.SellerId)
                {
                    updatesList.Add(new GameUpdateDto(type, entry.Key, JToken.FromObject(new
                        TradeDoneEventSellerDto(domainEvent.SellerId, domainEvent.BuyerId, domainEvent.SellerResources, domainEvent.BuyerResources.Values.Sum(), domainEvent.Offered, domainEvent.Desired))));
                }

                else if (entry.Value == domainEvent.BuyerId)
                {
                    updatesList.Add(new GameUpdateDto(type, entry.Key, JToken.FromObject(new
                        TradeDoneEventBuyerDto(domainEvent.SellerId, domainEvent.BuyerId, domainEvent.SellerResources.Values.Sum(), domainEvent.BuyerResources, domainEvent.Offered, domainEvent.Desired))));
                }

                else
                {
                    updatesList.Add(new GameUpdateDto(type, entry.Key, JToken.FromObject(new
                        TradeDoneEventPublicDto(domainEvent.SellerId, domainEvent.BuyerId, domainEvent.SellerResources.Values.Sum(), domainEvent.BuyerResources.Values.Sum(), domainEvent.Offered, 
                        domainEvent.Desired))));
                }
            }

            return updatesList;
        }

        private List<GameUpdateDto> BroadcastToAll(IDomainEvent domainEvent, CatanGameInstance game, string type)
        {
            var payload = JToken.FromObject(domainEvent);
            var updatesList = new List<GameUpdateDto>();

            foreach (var entry in game.PlayerTokens)
            {
                updatesList.Add(new GameUpdateDto(type, entry.Key, payload));
            }

            return updatesList;
        }
    }
}