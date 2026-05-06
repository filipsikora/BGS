using BGS.Shared.Data;
using BGS.Shared.Dtos;

namespace BGS.GameAbstractions.Interfaces
{
    public interface IGameInstance
    {
        CommandResponseDto Execute(CommandRequestDto request);
        object Query(string queryName, object? parameters = null);

        Guid GameId { get; }
        EnumGameInstanceState State { get; }
        int CurrentPlayers { get; }
        int DesiredPlayerNumber { get; }
    }
}