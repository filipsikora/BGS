namespace BGS.GameAbstractions.Interfaces
{
    public interface IGameFactory
    {
        (IGameInstance, int) CreateGame();
    }
}