using Catan.Application.Interfaces;
using Catan.Shared.Data;

namespace Catan.Application.UIMessages
{
    public sealed class ResourceSelectedMessage : IUIMessages
    {
        public EnumResourceType? Type { get; }
        public bool Selected { get; }
        public ResourceSelectedMessage(bool selected, EnumResourceType type)
        {
            Type = type;
            Selected = selected;
        }
    }

    public sealed class SelectionChangedMessage : IUIMessages
    {
        public bool ActionAvailable;
        public SelectionChangedMessage(bool actionAvailable)
        {
            ActionAvailable = actionAvailable;
        }
    }

    public sealed class DesiredCardsChangedMessage : IUIMessages
    {
        public bool HasDesired { get; }
        public DesiredCardsChangedMessage(bool hasDesired)
        {
            HasDesired = hasDesired;
        }
    }
}
