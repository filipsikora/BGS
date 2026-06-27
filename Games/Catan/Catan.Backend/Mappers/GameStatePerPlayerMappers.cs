using Catan.Core.Snapshots.Persistence;
using Catan.Shared.Dtos;

namespace Catan.Backend.Mappers
{
    public static class GameStatePerPlayerMappers
    {
        public static GameStatePerPlayerDto MapGameStatePerPlayerToDto(GameStatePerPlayerSnapshot snapshot, Guid gameId, Guid playerToken)
        {
            return new GameStatePerPlayerDto
            {
                GameId = gameId,
                PlayerToken = playerToken,
                Board = BoardMappers.MapFullBoardToDto(snapshot.Board),
                GameFlow = GameFlowMappers.MapFullGameFlowToDto(snapshot.GameFlow),
                Player = PlayerMappers.MapFullPlayerToDto(snapshot.Player.Data, snapshot.Player.Resources),
                OtherPlayers = PlayerMappers.MapOtherPlayersDataToDto(snapshot.OtherPlayers)
            };
        }
    }
}