using Catan.Shared.Data;
using Catan.Shared.Interfaces;

namespace Catan.Shared.Commands
{
    public class BankTradeCanceledCommand : ICommand { }

    public class BankTradeOfferedResourceSelectedCommand : ICommand
    {
        public EnumResourceType Type { get; }
        public BankTradeOfferedResourceSelectedCommand(EnumResourceType type)
        {
            Type = type;
        }
    }

    public class BankTradeDesiredResourceSelectedCommand : ICommand
    {
        public EnumResourceType? Type { get; }
        public BankTradeDesiredResourceSelectedCommand(EnumResourceType? type)
        {
            Type = type;
        }
    }

    public class BankTradeCommand : ICommand { }
}