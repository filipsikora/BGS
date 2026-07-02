using BGS.GameAbstractions.Models;
using BGS.Shared.Data;
using BGS.Shared.Dtos;

namespace BGS.GameAbstractions.Interfaces
{
    public interface IGameInstance
    {
        CommandResponseDto Execute(CommandRequestDto request);
        object Query(string queryName, object? parameters = null);
        JoinResult JoinGame(Guid? playerToken, string playerName);
        string GetGameStateDataString();

        Guid GameId { get; }
        EnumGameInstanceState State { get; }
        Dictionary<Guid, int> PlayerTokens { get; }
        int CurrentPlayers { get; }
        int DesiredPlayerNumber { get; }
        public EnumGames GameType { get; }
    }
}