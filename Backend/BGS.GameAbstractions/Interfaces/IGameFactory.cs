namespace BGS.GameAbstractions.Interfaces
{
    public interface IGameFactory
    {
        IGameInstance CreateGame(int playerNumber);

        int MinPlayers { get; }
        int MaxPlayers { get; }

    }
}