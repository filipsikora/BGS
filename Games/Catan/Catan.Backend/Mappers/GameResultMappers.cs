using BGS.Shared.Dtos;
using Catan.Application;
using Catan.Application.Interfaces;
using Catan.Application.UIMessages;
using Catan.Shared.Dtos.UiMessages;
using Catan.Shared.Dtos.UIMessages;
using Catan.Shared.Interfaces;

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

        public static CommandResponseDto MapGameResultToDto(GameResult result)
        {
            return new CommandResponseDto
            {
                Success = result.Success,
                NextPhase = result.NextPhase != null ? result.NextPhase.ToString() : null,

                UiMessages = result.GetUIMessagesList().Select(MapUiMessageToWrapperDto).ToList(),
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
                VillagePlacedMessage e => new VillagePlacedMessageDto(e.VertexId, e.OwnerId),
                RoadPlacedMessage e => new RoadPlacedMessageDto(e.EdgeId, e.OwnerId),
                TownPlacedMessage e => new TownPlacedMessageDto(e.VertexId, e.OwnerId),
                DevelopmentCardBoughtMessage e => new DevelopmentCardBoughtMessageDto(e.CardId),
                RobberPlacedMessage e => new RobberPlacedMessageDto(e.HexId),
                PlayerStateChangedMessage e => new PlayerStateChangedDto(e.PlayerId),
                _ => throw new Exception($"Unknown UI message: {message}")
            };
        }
    }
}
