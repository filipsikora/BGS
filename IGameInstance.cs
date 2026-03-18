namespace BGS.GameAbstractions;
{
    public interface IGameInstance
    {
        object Execute(object command);
    }
}