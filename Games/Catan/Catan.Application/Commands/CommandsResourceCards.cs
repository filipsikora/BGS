using Catan.Shared.Data;
using Catan.Application.Interfaces;

namespace Catan.Application.Commands
{
    public class ResourceCardSelectedCommand : ICommand
    {
        public EnumResourceType Type { get; }
        public bool IsSelected { get; }
        public ResourceCardSelectedCommand(bool isSelected, EnumResourceType type)
        {
            IsSelected = isSelected;
            Type = type;
        }
    }
}