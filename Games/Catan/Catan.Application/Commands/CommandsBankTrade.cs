using Catan.Shared.Data;
using Catan.Application.Interfaces;

namespace Catan.Application.Commands
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