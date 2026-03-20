using Catan.Shared.Data;
using Catan.Shared.Interfaces;

namespace Catan.Shared.Commands
{
    public class BankTradeCanceledCommand : ICommand { }

    public class BankTradeOfferedResourceSelectedCommand : ICommand
    {
        public EnumResourceTypes Type { get; }
        public BankTradeOfferedResourceSelectedCommand(EnumResourceTypes type)
        {
            Type = type;
        }
    }

    public class BankTradeDesiredResourceSelectedCommand : ICommand
    {
        public EnumResourceTypes? Type { get; }
        public BankTradeDesiredResourceSelectedCommand(EnumResourceTypes? type)
        {
            Type = type;
        }
    }

    public class BankTradeCommand : ICommand { }
}