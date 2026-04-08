using Catan.Application;
using Catan.Application.Interfaces;
using Catan.Application.UIMessages;
using Catan.Core.DomainEvents;
using Catan.Core.Interfaces;
using Catan.Shared.Data;
using Catan.Shared.Dtos;
using Catan.Shared.Dtos.DomainEvents;
using Catan.Shared.Dtos.UiMessages;
using Catan.Shared.Interfaces;

namespace Catan.Backend.Mappers
{
    public static class GameResultMappers
    {
        public static UiMessageDto MapUiMessageToWrapperDto(IUIMessages message)
        {
            return new UiMessageDto
            {
                Type = MapUiMessageToEnum(message),
                Data = MapUiMessageToDto(message)
            };
        }

        public static DomainEventDto MapDomainMessageToWrapperDto(IDomainEvent message)
        {
            return new DomainEventDto
            {
                Type = MapDomainEventsToEnum(message),
                Data = MapDomainEventToDto(message)
            };
        }

        public static CommandResponseDto MapGameResultToDto(GameResult result)
        {
            return new CommandResponseDto
            {
                Success = result.Success,
                NextPhase = result.NextPhase,

                UiMessages = result.GetUIMessagesList().Select(MapUiMessageToWrapperDto).ToList(),
                DomainMessages = result.GetDomainEventsList().Select(MapDomainMessageToWrapperDto).ToList()
            };
        }

        private static EnumUiMessages MapUiMessageToEnum(IUIMessages message)
        {
            return message switch
            {
                VertexHighlightedMessage => EnumUiMessages.VertexHighlightedMessage,
                EdgeHighlightedMessage => EnumUiMessages.EdgeHighlightedMessage,
                BuildOptionsSentMessage => EnumUiMessages.BuildOptionsSentMessage,
                LogMessageMessage => EnumUiMessages.LogMessageMessage,
                ActionRejectedMessage => EnumUiMessages.ActionRejectedMessage,
                ResourceSelectedMessage => EnumUiMessages.ResourceSelectedMessage,
                SelectionChangedMessage => EnumUiMessages.SelectionChangedMessage,
                DesiredCardsChangedMessage => EnumUiMessages.DesiredCardsChangedMessage,
                PlayerSelectedToDiscardMessage => EnumUiMessages.PlayerSelectedToDiscardMessage,
                PotentialVictimsFoundMessage => EnumUiMessages.PotentialVictimsFoundMessage,
                BankTradeRatioChangedMessage => EnumUiMessages.BankTradeRatioChangedMessage,
                TurnNumberChangedMessage => EnumUiMessages.TurnNumberChangedMessage,
                DiceRollChangedMessage => EnumUiMessages.DiceRollChangedMessage,
                _ => throw new Exception($"Unknown UI message: {message}")
            };
        }

        private static EnumDomainEvents MapDomainEventsToEnum(IDomainEvent message)
        {
            return message switch
            {
                VillagePlacedEvent => EnumDomainEvents.VillagePlacedEvent,
                RoadPlacedEvent => EnumDomainEvents.RoadPlacedEvent,
                TownPlacedEvent => EnumDomainEvents.TownPlacedEvent,
                DevelopmentCardBoughtEvent => EnumDomainEvents.DevelopmentCardBoughtEvent,
                RobberPlacedEvent => EnumDomainEvents.RobberPlacedEvent,
                PlayerStateChangedEvent => EnumDomainEvents.PlayerStateChangedEvent
            };
        }

        private static IUiMessageDto MapUiMessageToDto(IUIMessages message)
        {
            return message switch
            {
                VertexHighlightedMessage m => new VertexHighlightedDto(m.VertexId),
                EdgeHighlightedMessage m => new EdgeHighlightedDto(m.EdgeId),
                BuildOptionsSentMessage m => new BuildOptionsSentDto(m.CanBuildVillage, m.CanBuildRoad, m.CanUpgradeVillage),
                LogMessageMessage m => new LogMessageDto(m.Type, m.Message, m.Time),
                ActionRejectedMessage m => new ActionRejectedDto(m.PlayerId, m.Reason),
                ResourceSelectedMessage m => new ResourceSelectedDto(m.Selected, m.Type),
                SelectionChangedMessage m => new SelectionChangedDto(m.ActionAvailable),
                DesiredCardsChangedMessage m => new DesiredCardsChangedDto(m.HasDesired),
                PlayerSelectedToDiscardMessage m => new PlayerSelectedToDiscardDto(m.PlayerId),
                PotentialVictimsFoundMessage m => new PotentialVictimsFoundDto(m.VictimsIds),
                BankTradeRatioChangedMessage m => new BankTradeRatioChangedDto(m.Ratio, m.PossibleForPlayer, m.Resource),
                TurnNumberChangedMessage m => new TurnNumberChangedDto(m.NewTurnNumber),
                DiceRollChangedMessage m => new DiceRollChangedDto(m.RolledNumber),
                _ => throw new Exception($"Unknown UI message: {message}")
            };
        }

        private static IDomainEventDto MapDomainEventToDto(IDomainEvent message)
        {
            return message switch
            {
                VillagePlacedEvent m => new VillagePlacedDto(m.VertexId, m.OwnerId),
                RoadPlacedEvent m => new RoadPlacedDto(m.EdgeId, m.OwnerId),
                TownPlacedEvent m => new TownPlacedDto(m.VertexId, m.OwnerId),
                DevelopmentCardBoughtEvent m => new DevelopmentCardBoughtDto(m.CardId),
                RobberPlacedEvent m => new RobberPlacedDto(m.HexId),
                PlayerStateChangedEvent m => new PlayerStateChangedDto(m.PlayerId),
                _ => throw new Exception($"Unknown domain event: {message}")
            };
        }
    }
}
