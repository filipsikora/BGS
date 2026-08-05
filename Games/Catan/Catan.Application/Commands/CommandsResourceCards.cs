using Catan.Application.Interfaces;
using Catan.Core.Models;
using Catan.Shared.Data;

namespace Catan.Application.Commands
{
    public class CardsSelectedCommand(Dictionary<EnumResourceType, int> resources) : ICommand
    {
        public ResourceCostOrStock Resources = ResourceCostOrStock.FromDictionary(resources);
    }
}