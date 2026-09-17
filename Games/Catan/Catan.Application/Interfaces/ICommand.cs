namespace Catan.Application.Interfaces
{
    public interface ICommand
    {
        bool RequiresPlayerTurn { get; }
    }
}