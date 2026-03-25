using Catan.Application;
using Catan.Shared.Dtos;

namespace Catan.Backend.Mappers
{
    public static class GameResultMappers
    {
        public static object MapUiMessageToDto(object message)
        {
            return new
            {
                Type = message.GetType().Name,
                Data = message
            };
        }

        public static object MapDomainMessageToDto(object message)
        {
            return new
            {
                Type = message.GetType().Name,
                Data = message
            };
        }

        public static CommandResponseDto MapGameResultToDto(GameResult result)
        {
            return new CommandResponseDto
            {
                Success = result.Success,
                NextPhase = EnumMappers.MapGamePhaseToDto(result.NextPhase),

                UiMessages = result.GetUIMessagesList().Select(MapUiMessageToDto).ToList(),
                DomainMessages = result.GetDomainEventsList().Select(MapDomainMessageToDto).ToList()
            };
        }
    }
}
