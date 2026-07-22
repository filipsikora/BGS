using Catan.Application.Interfaces;
using Catan.Core.Models;
using Catan.Shared.Data;

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

    public class CardsSelectedCommand(Dictionary<EnumResourceType, int> resources) : ICommand
    {
        public ResourceCostOrStock Resources = ResourceCostOrStock.FromDictionary(resources);
    }
}