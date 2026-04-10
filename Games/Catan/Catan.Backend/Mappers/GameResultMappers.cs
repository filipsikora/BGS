using Catan.Application;
using Catan.Application.Interfaces;
using Catan.Application.UIMessages;
using Catan.Core.DomainEvents;
using Catan.Core.Interfaces;
using Catan.Shared.Dtos.DomainEvents;
using Catan.Shared.Dtos.UiMessages;
using Catan.Shared.Interfaces;
using BGS.Shared.Dtos;

namespace Catan.Backend.Mappers
{
    public static class GameResultMappers
    {
        public static UiMessageDto MapUiMessageToWrapperDto(IUIMessages message)
        {
            return new UiMessageDto
            {
                Type = message.GetType().Name,
                Data = MapUiMessageToDto(message)
            };
        }

        public static DomainEventDto MapDomainMessageToWrapperDto(IDomainEvent message)
        {
            return new DomainEventDto
            {
                Type = message.GetType().Name,
                Data = MapDomainEventToDto(message)
            };
        }

        public static CommandResponseDto MapGameResultToDto(GameResult result)
        {
            return new CommandResponseDto
            {
                Success = result.Success,
                NextPhase = result.NextPhase != null ? result.NextPhase.ToString() : null,

                UiMessages = result.GetUIMessagesList().Select(MapUiMessageToWrapperDto).ToList(),
                DomainMessages = result.GetDomainEventsList().Select(MapDomainMessageToWrapperDto).ToList()
            };
        }

        private static IUiMessageDto MapUiMessageToDto(IUIMessages message)
        {
            return message switch
            {
                VertexHighlightedMessage m => new VertexHighlightedDto(m.VertexId),
                EdgeHighlightedMessage m => new EdgeHighlightedDto(m.EdgeId),
                BuildOptionsSentMessage m => new BuildOptionsSentDto(m.CanBuildVillage, m.CanBuildRoad, m.CanUpgradeVillage),
                LogMessageMessage m => new LogMessageDto(m.Type.ToString(), m.Message, m.Time),
                ActionRejectedMessage m => new ActionRejectedDto(m.PlayerId, m.Reason.ToString()),
                ResourceSelectedMessage m => new ResourceSelectedDto(m.Selected, m.Type.ToString()),
                SelectionChangedMessage m => new SelectionChangedDto(m.ActionAvailable),
                DesiredCardsChangedMessage m => new DesiredCardsChangedDto(m.HasDesired),
                PlayerSelectedToDiscardMessage m => new PlayerSelectedToDiscardDto(m.PlayerId),
                PotentialVictimsFoundMessage m => new PotentialVictimsFoundDto(m.VictimsIds),
                BankTradeRatioChangedMessage m => new BankTradeRatioChangedDto(m.Ratio, m.PossibleForPlayer, m.Resource.ToString()),
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
