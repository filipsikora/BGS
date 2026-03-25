using BGS.GameAbstractions.Interfaces;
using Catan.Shared.Data;

namespace BGS.Backend.Interfaces
{
    public interface IGameFactoryMapper
    {
        IGameFactory GetFactory(EnumGames game);
    }
}