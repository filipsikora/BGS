using BGS.Shared.Dtos;
using Catan.Application;
using Catan.Application.Interfaces;
using Catan.Application.UIMessages;
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
                Type = message.GetType().Name,
                Data = MapUiMessageToDto(message)
            };
        }

        public static CommandResponseDto MapGameResultToDto(GameResult result)
        {
            return new CommandResponseDto
            {
                Success = result.Success,
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
                ActionRejectedMessage m => new ActionRejectedDto(m.PlayerId, m.Reason.ToString()),
                PotentialVictimsFoundMessage m => new PotentialVictimsFoundDto(m.VictimsIds),
                BankTradeRatioChangedMessage m => new BankTradeRatioChangedDto(m.Ratio, m.PossibleForPlayer, m.Resource.ToString()),
                _ => throw new Exception($"Unknown UI message: {message}")
            };
        }
    }
}
