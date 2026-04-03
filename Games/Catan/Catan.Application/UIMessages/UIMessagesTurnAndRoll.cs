using Catan.Application.Interfaces;

namespace Catan.Application.UIMessages
{
    public sealed class TurnNumberChangedMessage : IUIMessages
    {
        public int NewTurnNumber;
        public TurnNumberChangedMessage(int newTurnNumber)
        {
            NewTurnNumber = newTurnNumber;
        }
    }

    public sealed class DiceRollChangedMessage : IUIMessages
    {
        public int RolledNumber;
        public DiceRollChangedMessage(int rolledNumber)
        {
            RolledNumber = rolledNumber;
        }
    }
}
