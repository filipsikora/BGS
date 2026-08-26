using BGS.Shared.Dtos;
using Catan.Backend.GameManagement;
using Catan.Backend.Mappers;
using Catan.Core.DomainEvents;
using Catan.Core.Interfaces;
using Catan.Shared.Data;
using Catan.Shared.Dtos.DomainEvents;
using Newtonsoft.Json.Linq;
using System.Dynamic;

namespace Catan.Backend.Helpers
{
    public class DomainEventsDispatcher
    {
        public List<GameUpdateDto> Dispatch(IDomainEvent domainEvent, CatanGameInstance game)
        {
            return domainEvent switch
            {
                BankTradeDoneEvent e => DispatchBankTradeDoneEvent(e, game),
                RolledNumberChangedEvent e => BroadcastToAll(new RolledNumberChangedEventDto(e.NewRolledNumber), game, EnumDomainEventsDto.RolledNumberChangedEventDto),
                PlayerResourcesReceivedEvent e => DispatchPlayerResourcesReceivedEvent(e, game),
                PhaseChangedEvent e => BroadcastToAll(new PhaseChangedEventDto(e.Phase, e.PlayersToMove), game, EnumDomainEventsDto.PhaseChangedEventDto),
                CardsDiscardedEvent e => DispatchCardsDiscardedEvent(e, game),
                PlayersToMoveChangedEvent e => BroadcastToAll(new PlayersToMoveChangedEventDto(e.PlayersToMove), game, EnumDomainEventsDto.PlayersToMoveChangedEventDto),
                CardStolenEvent e => DispatchCardStolenEvent(e, game),
                DevCardUsedEvent e => DispatchDevCardUsedEvent(e, game),
                VillagePlacedEvent e => DispatchVillagePlacedEvent(e, game),
                RoadPlacedEvent e => DispatchRoadPlacedEvent(e, game),
                TownPlacedEvent e => DispatchTownPlacedEvent(e, game),
                GameWonEvent e => BroadcastToAll(new GameWonEventDto(e.PlayerId, e.PlayerScoresToIds), game, EnumDomainEventsDto.GameWonEventDto),
                CardsStolenEvent e => DispatchCardsStolenEvent(e, game),
                RoadChampionChangedEvent e => BroadcastToAll(new RoadChampionChangedEventDto(e.OldChampionId, e.NewChampionId, e.OldChampionExtraPoints, e.NewChampionExtraPoints, e.OldChampionPoints, e.NewChampionPoints), game, EnumDomainEventsDto.RoadChampionChangedEventDto),
                KnightChampionChangedEvent e => BroadcastToAll(new KnightChampionChangedEventDto(e.OldChampionId, e.NewChampionId, e.OldChampionExtraPoints, e.NewChampionExtraPoints, e.OldChampionPoints, e.NewChampionPoints), game, EnumDomainEventsDto.KnightChampionChangedEventDto),
                DevCardBoughtEvent e => DispatchDevCardBoughtEvent(e, game),
                VictoryCardUsedEvent e => BroadcastToAll(new VictoryCardUsedEventDto(e.PlayerId, e.ExtraPoints, e.VictoryCardsUsed), game, EnumDomainEventsDto.VictoryCardUsedEventDto),
                KnightCardUsedEvent e => BroadcastToAll(new KnightCardUsedEventDto(e.PlayerId, e.KnightCardsUsed), game, EnumDomainEventsDto.KnightCardUsedEventDto),
                TradeDoneEvent e => DispatchTradeDoneEvent(e, game),
                RobberPlacedEvent e => BroadcastToAll(new RobberPlacedEventDto(e.HexId, e.CanSteal), game, EnumDomainEventsDto.RobberPlacedEventDto),
                DevCardPlayabilityChangedEvent e => DispatchDevCardPlayabilityChangedEvent(e, game),
                _ => throw new NotSupportedException($"Unknown domain event: {domainEvent.GetType().Name}")
            };
        }

        private List<GameUpdateDto> DispatchBankTradeDoneEvent(BankTradeDoneEvent domainEvent, CatanGameInstance game)
        {
            var updatesList = new List<GameUpdateDto>();

            foreach (var entry in game.PlayerTokens)
            {
                if (entry.Value == domainEvent.PlayerId)
                {
                    updatesList.Add(new GameUpdateDto(EnumDomainEventsDto.BankTradeDoneEventPrivateDto.ToString(), entry.Key, JToken.FromObject(new BankTradeDoneEventPrivateDto(
                        domainEvent.PlayerId,
                        domainEvent.Offered,
                        domainEvent.Desired,
                        domainEvent.Ratio,
                        domainEvent.Bank,
                        domainEvent.PlayerResources,
                        domainEvent.PlayerResources.Values.Sum()))));
                }
                else
                {
                    updatesList.Add(new GameUpdateDto(EnumDomainEventsDto.CardsDiscardedPublicEventDto.ToString(), entry.Key, JToken.FromObject(new BankTradeDoneEventPublicDto(
                        domainEvent.PlayerId,
                        domainEvent.Offered,
                        domainEvent.Desired,
                        domainEvent.Ratio,
                        domainEvent.Bank,
                        domainEvent.PlayerResources.Values.Sum()))));
                }
            }

            return updatesList;
        }

        private List<GameUpdateDto> DispatchDevCardUsedEvent(DevCardUsedEvent domainEvent, CatanGameInstance game)
        {
            var updatesList = new List<GameUpdateDto>();

            foreach (var entry in game.PlayerTokens)
            {
                if (entry.Value == domainEvent.PlayerId)
                {
                    updatesList.Add(new GameUpdateDto(EnumDomainEventsDto.DevCardUsedEventPrivateDto.ToString(), entry.Key, JToken.FromObject(new DevCardUsedEventPrivateDto(
                        domainEvent.PlayerId,
                        domainEvent.CardId,
                        domainEvent.CardType,
                        domainEvent.DevCardNumber,
                        domainEvent.DevCards.Select(PlayerMappers.MapDevCardToDto).ToList()))));
                }
                else
                {
                    updatesList.Add(new GameUpdateDto(EnumDomainEventsDto.DevCardUsedEventPublicDto.ToString(), entry.Key, JToken.FromObject(new DevCardUsedEventPublicDto(
                        domainEvent.PlayerId,
                        domainEvent.DevCardNumber))));
                }
            }

            return updatesList;
        }

        private List<GameUpdateDto> DispatchCardsDiscardedEvent(CardsDiscardedEvent domainEvent, CatanGameInstance game)
        {
            var updatesList = new List<GameUpdateDto>();

            foreach (var entry in game.PlayerTokens)
            {
                if (entry.Value == domainEvent.PlayerId)
                {
                    updatesList.Add(new GameUpdateDto(EnumDomainEventsDto.CardsDiscardedEventPrivateDto.ToString(), entry.Key, JToken.FromObject(new CardsDiscardedEventPrivateDto(
                        domainEvent.PlayerId,
                        domainEvent.Resources,
                        domainEvent.PlayerResources,
                        domainEvent.Bank,
                        domainEvent.PlayerResources.Values.Sum()))));
                }
                else
                {
                    updatesList.Add(new GameUpdateDto(EnumDomainEventsDto.CardsDiscardedPublicEventDto.ToString(), entry.Key, JToken.FromObject(new CardsDiscardedPublicEventDto(
                        domainEvent.PlayerId,
                        domainEvent.Resources,
                        domainEvent.Resources.Values.Sum(),
                        domainEvent.Bank))));
                }
            }

            return updatesList;
        }

        private List<GameUpdateDto> DispatchPlayerResourcesReceivedEvent(PlayerResourcesReceivedEvent domainEvent, CatanGameInstance game)
        {
            var updatesList = new List<GameUpdateDto>();

            foreach (var entry in game.PlayerTokens)
            {
                if (entry.Value == domainEvent.PlayerId)
                {
                    updatesList.Add(new GameUpdateDto(EnumDomainEventsDto.PlayerResourcesReceivedEventPrivateDto.ToString(), entry.Key, JToken.FromObject(new PlayerResourcesReceivedEventPrivateDto(
                        domainEvent.PlayerId,
                        domainEvent.ResourcesChange,
                        domainEvent.PlayerResources,
                        domainEvent.Bank,
                        domainEvent.PlayerResources.Values.Sum()))));
                }
                else
                {
                    updatesList.Add(new GameUpdateDto(EnumDomainEventsDto.PlayerResourcesReceivedEventPublicDto.ToString(), entry.Key, JToken.FromObject(new PlayerResourcesReceivedEventPublicDto(
                        domainEvent.PlayerId,
                        domainEvent.ResourcesChange,
                        domainEvent.PlayerResources.Values.Sum(),
                        domainEvent.Bank))));
                }
            }

            return updatesList;
        }

        private List<GameUpdateDto> DispatchCardStolenEvent(CardStolenEvent domainEvent, CatanGameInstance game)
        {
            var updatesList = new List<GameUpdateDto>();

            foreach (var entry in game.PlayerTokens)
            {
                if (entry.Value == domainEvent.ThiefId)
                {
                    updatesList.Add(new GameUpdateDto(EnumDomainEventsDto.CardStolenEventThiefDto.ToString(), entry.Key, JToken.FromObject(new CardStolenEventThiefDto(
                        domainEvent.Resource,
                        domainEvent.ThiefId,
                        domainEvent.VictimId,
                        domainEvent.ThiefResources,
                        domainEvent.VictimResources.Values.Sum(),
                        domainEvent.ThiefResources.Values.Sum()))));
                }
                else if (entry.Value == domainEvent.VictimId)
                {
                    updatesList.Add(new GameUpdateDto(EnumDomainEventsDto.CardStolenEventVictimDto.ToString(), entry.Key, JToken.FromObject(new CardStolenEventVictimDto(
                        domainEvent.Resource,
                        domainEvent.ThiefId,
                        domainEvent.VictimId,
                        domainEvent.ThiefResources.Values.Sum(),
                        domainEvent.VictimResources,
                        domainEvent.VictimResources.Values.Sum()))));
                }
                else
                {
                    updatesList.Add(new GameUpdateDto(EnumDomainEventsDto.CardStolenEventPublicDto.ToString(), entry.Key, JToken.FromObject(new CardStolenEventPublicDto(
                        domainEvent.Resource,
                        domainEvent.ThiefId,
                        domainEvent.VictimId,
                        domainEvent.ThiefResources.Values.Sum(),
                        domainEvent.VictimResources.Values.Sum()))));
                }
            }

            return updatesList;
        }

        private List<GameUpdateDto> DispatchVillagePlacedEvent(VillagePlacedEvent domainEvent, CatanGameInstance game)
        {
            var updatesList = new List<GameUpdateDto>();

            foreach (var entry in game.PlayerTokens)
            {
                if (entry.Value == domainEvent.OwnerId)
                {
                    updatesList.Add(new GameUpdateDto(EnumDomainEventsDto.VillagePlacedEventPrivateDto.ToString(), entry.Key, JToken.FromObject(new VillagePlacedEventPrivateDto(
                        domainEvent.VertexId,
                        domainEvent.OwnerId,
                        domainEvent.Points,
                        domainEvent.Resources,
                        domainEvent.Bank.ToDictionary(),
                        domainEvent.Resources.Values.Sum(),
                        domainEvent.BuildingsLeft.ToDictionary()))));
                }
                else
                {
                    updatesList.Add(new GameUpdateDto(EnumDomainEventsDto.VillagePlacedEventPublicDto.ToString(), entry.Key, JToken.FromObject(new VillagePlacedEventPublicDto(
                        domainEvent.VertexId,
                        domainEvent.OwnerId,
                        domainEvent.Points,
                        domainEvent.Resources.Values.Sum(),
                        domainEvent.Bank.ToDictionary(),
                        domainEvent.BuildingsLeft))));
                }
            }

            return updatesList;
        }

        private List<GameUpdateDto> DispatchRoadPlacedEvent(RoadPlacedEvent domainEvent, CatanGameInstance game)
        {
            var updatesList = new List<GameUpdateDto>();

            foreach (var entry in game.PlayerTokens)
            {
                if (entry.Value == domainEvent.OwnerId)
                {
                    updatesList.Add(new GameUpdateDto(EnumDomainEventsDto.RoadPlacedEventPrivateDto.ToString(), entry.Key, JToken.FromObject(new RoadPlacedEventPrivateDto(
                        domainEvent.EdgeId,
                        domainEvent.OwnerId,
                        domainEvent.Resources,
                        domainEvent.Bank.ToDictionary(),
                        domainEvent.Resources.Values.Sum(),
                        domainEvent.BuildingsLeft))));
                }
                else
                {
                    updatesList.Add(new GameUpdateDto(EnumDomainEventsDto.RoadPlacedEventPublicDto.ToString(), entry.Key, JToken.FromObject(new RoadPlacedEventPublicDto(
                        domainEvent.EdgeId,
                        domainEvent.OwnerId,
                        domainEvent.Resources.Values.Sum(),
                        domainEvent.Bank.ToDictionary(),
                        domainEvent.BuildingsLeft))));
                }
            }

            return updatesList;
        }

        private List<GameUpdateDto> DispatchTownPlacedEvent(TownPlacedEvent domainEvent, CatanGameInstance game)
        {
            var updatesList = new List<GameUpdateDto>();

            foreach (var entry in game.PlayerTokens)
            {
                if (entry.Value == domainEvent.OwnerId)
                {
                    updatesList.Add(new GameUpdateDto(EnumDomainEventsDto.TownPlacedEventPrivateDto.ToString(), entry.Key, JToken.FromObject(new TownPlacedEventPrivateDto(
                        domainEvent.VertexId,
                        domainEvent.OwnerId,
                        domainEvent.Points,
                        domainEvent.Resources,
                        domainEvent.Bank.ToDictionary(),
                        domainEvent.Resources.Values.Sum(),
                        domainEvent.BuildingsLeft))));
                }
                else
                {
                    updatesList.Add(new GameUpdateDto(EnumDomainEventsDto.TownPlacedEventPublicDto.ToString(), entry.Key, JToken.FromObject(new TownPlacedEventPublicDto(
                        domainEvent.VertexId,
                        domainEvent.OwnerId,
                        domainEvent.Points,
                        domainEvent.Resources.Values.Sum(),
                        domainEvent.Bank.ToDictionary(),
                        domainEvent.BuildingsLeft))));
                }
            }

            return updatesList;
        }

        private List<GameUpdateDto> DispatchCardsStolenEvent(CardsStolenEvent domainEvent, CatanGameInstance game)
        {
            var updatesList = new List<GameUpdateDto>();

            foreach (var entry in game.PlayerTokens)
            {
                if (entry.Value == domainEvent.ThiefId)
                {
                    updatesList.Add(new GameUpdateDto(EnumDomainEventsDto.CardsStolenEventThiefDto.ToString(), entry.Key, JToken.FromObject(new CardsStolenEventThiefDto(
                        domainEvent.Resource,
                        domainEvent.Quantity,
                        domainEvent.ThiefId,
                        domainEvent.VictimId,
                        domainEvent.ThiefResources,
                        domainEvent.VictimResources.Values.Sum(),
                        domainEvent.ThiefResources.Values.Sum()))));
                }
                else if (entry.Value == domainEvent.VictimId)
                {
                    updatesList.Add(new GameUpdateDto(EnumDomainEventsDto.CardsStolenEventVictimDto.ToString(), entry.Key, JToken.FromObject(new CardsStolenEventVictimDto(
                        domainEvent.Resource,
                        domainEvent.Quantity,
                        domainEvent.ThiefId,
                        domainEvent.VictimId,
                        domainEvent.ThiefResources.Values.Sum(),
                        domainEvent.VictimResources,
                        domainEvent.VictimResources.Values.Sum()))));
                }
                else
                {
                    updatesList.Add(new GameUpdateDto(EnumDomainEventsDto.CardsStolenEventPublicDto.ToString(), entry.Key, JToken.FromObject(new CardsStolenEventPublicDto(
                        domainEvent.Resource,
                        domainEvent.Quantity,
                        domainEvent.ThiefId,
                        domainEvent.VictimId,
                        domainEvent.ThiefResources.Values.Sum(),
                        domainEvent.VictimResources.Values.Sum()))));
                }
            }

            return updatesList;
        }

        private List<GameUpdateDto> DispatchDevCardBoughtEvent(DevCardBoughtEvent domainEvent, CatanGameInstance game)
        {
            var updatesList = new List<GameUpdateDto>();

            foreach (var entry in game.PlayerTokens)
            {
                if (entry.Value == domainEvent.PlayerId)
                {
                    updatesList.Add(new GameUpdateDto(EnumDomainEventsDto.DevCardBoughtEventPrivateDto.ToString(), entry.Key, JToken.FromObject(new DevCardBoughtEventPrivateDto(
                        domainEvent.PlayerId,
                        domainEvent.CardId,
                        domainEvent.DevCardType,
                        domainEvent.DevCardNumber,
                        domainEvent.Resources,
                        false,
                        domainEvent.Resources.Values.Sum()))));
                }
                else
                {
                    updatesList.Add(new GameUpdateDto(EnumDomainEventsDto.DevCardBoughtEventPublicDto.ToString(), entry.Key, JToken.FromObject(new DevCardBoughtEventPublicDto(
                        domainEvent.PlayerId,
                        domainEvent.DevCardNumber,
                        domainEvent.Resources.Values.Sum()))));
                }
            }

            return updatesList;
        }

        private List<GameUpdateDto> DispatchTradeDoneEvent(TradeDoneEvent domainEvent, CatanGameInstance game)
        {
            var updatesList = new List<GameUpdateDto>();

            foreach (var entry in game.PlayerTokens)
            {
                if (entry.Value == domainEvent.SellerId)
                {
                    updatesList.Add(new GameUpdateDto(EnumDomainEventsDto.TradeDoneEventSellerDto.ToString(), entry.Key, JToken.FromObject(new TradeDoneEventSellerDto(
                        domainEvent.SellerId,
                        domainEvent.BuyerId,
                        domainEvent.SellerResources,
                        domainEvent.BuyerResources.Values.Sum(),
                        domainEvent.Offered,
                        domainEvent.Desired,
                        domainEvent.SellerResources.Values.Sum()))));
                }
                else if (entry.Value == domainEvent.BuyerId)
                {
                    updatesList.Add(new GameUpdateDto(EnumDomainEventsDto.TradeDoneEventBuyerDto.ToString(), entry.Key, JToken.FromObject(new TradeDoneEventBuyerDto(
                        domainEvent.SellerId,
                        domainEvent.BuyerId,
                        domainEvent.SellerResources.Values.Sum(),
                        domainEvent.BuyerResources,
                        domainEvent.Offered,
                        domainEvent.Desired,
                        domainEvent.BuyerResources.Values.Sum()))));
                }
                else
                {
                    updatesList.Add(new GameUpdateDto(EnumDomainEventsDto.TradeDoneEventPublicDto.ToString(), entry.Key, JToken.FromObject(new TradeDoneEventPublicDto(
                        domainEvent.SellerId,
                        domainEvent.BuyerId,
                        domainEvent.SellerResources.Values.Sum(),
                        domainEvent.BuyerResources.Values.Sum(),
                        domainEvent.Offered,
                        domainEvent.Desired))));
                }
            }

            return updatesList;
        }

        private List<GameUpdateDto> DispatchDevCardPlayabilityChangedEvent(DevCardPlayabilityChangedEvent domainEvent, CatanGameInstance game)
        {
            var updatesList = new List<GameUpdateDto>();

            foreach (var entry in game.PlayerTokens)
            {
                if (entry.Value == domainEvent.PlayerId)
                {
                    updatesList.Add(new GameUpdateDto(EnumDomainEventsDto.DevCardPlayabilityChangedEventPrivateDto.ToString(), entry.Key, JToken.FromObject(new DevCardPlayabilityChangedEventPrivateDto(
                        domainEvent.DevCardsPlayable))));
                }
                else
                {
                    updatesList.Add(new GameUpdateDto(EnumDomainEventsDto.DevCardPlayabilityChangedEventPublicDto.ToString(), entry.Key, JToken.FromObject(new DevCardPlayabilityChangedEventPublicDto())));
                }
            }

            return updatesList;
        }

        private List<GameUpdateDto> BroadcastToAll<T>(T dto, CatanGameInstance game, EnumDomainEventsDto dtoType)
        {
            var payload = JToken.FromObject(dto);
            var updatesList = new List<GameUpdateDto>();

            foreach (var entry in game.PlayerTokens)
            {
                updatesList.Add(new GameUpdateDto(dtoType.ToString(), entry.Key, payload));
            }

            return updatesList;
        }
    }
}