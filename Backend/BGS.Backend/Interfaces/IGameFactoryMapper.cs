using BGS.GameAbstractions.Interfaces;
using BGS.Shared.Data;

namespace BGS.Backend.Interfaces
{
    public interface IGameFactoryMapper
    {
        IGameFactory GetFactory(EnumGames game);
    }
}