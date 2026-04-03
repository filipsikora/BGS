using Catan.Application;
using Catan.Application.Interfaces;
using Catan.Application.UIMessages;
using Catan.Core.DomainEvents;
using Catan.Core.Interfaces;
using Catan.Shared.Data;
using Catan.Shared.Dtos;

namespace Catan.Backend.Mappers
{
    public static class GameResultMappers
    {
        public static UiMessageDto MapUiMessageToDto(IUIMessages message)
        {
            return new UiMessageDto
            {
                Type = MapUiMessageToEnum(message),
                Data = message
            };
        }

        public static DomainEventDto MapDomainMessageToDto(IDomainEvent message)
        {
            return new DomainEventDto
            {
                Type = MapDomainEventsToEnum(message),
                Data = message
            };
        }

        public static CommandResponseDto MapGameResultToDto(GameResult result)
        {
            return new CommandResponseDto
            {
                Success = result.Success,
                NextPhase = result.NextPhase,

                UiMessages = result.GetUIMessagesList().Select(MapUiMessageToDto).ToList(),
                DomainMessages = result.GetDomainEventsList().Select(MapDomainMessageToDto).ToList()
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
    }
}
