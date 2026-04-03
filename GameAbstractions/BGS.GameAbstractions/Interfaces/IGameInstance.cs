namespace BGS.GameAbstractions.Interfaces
{
    public interface IGameInstance
    {
        object Execute(object request);
        object Query(object request);
    }
}