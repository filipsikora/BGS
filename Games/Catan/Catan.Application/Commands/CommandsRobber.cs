using Catan.Shared.Data;
using Catan.Application.Interfaces;

namespace Catan.Application.Commands
{
    public class VictimChosenCommand : ICommand
    {
        public int VictimId { get; }
        public VictimChosenCommand(int victimId)
        {
            VictimId = victimId;
        }
    }

    public class DiscardingAcceptedCommand : ICommand { }

    public class StolenCardSelectedCommand : ICommand
    {
        public EnumResourceType Type { get; }
        public StolenCardSelectedCommand(EnumResourceType type)
        {
            Type = type;
        }
    }

    public sealed class TryGetDiscardingVictimCommand : ICommand { }
}