using BGS.GameAbstractions.Interfaces;

namespace BGS.Backend
{
    public class FakeGameInstance : IGameInstance
    {
        public object Execute(object command)
        {
            return "Backend works!";
        }
    }
}