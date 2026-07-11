using Catan.Shared.Data;

namespace Catan.Core.Interfaces
{
    public interface IDomainEvent
    {
        public EnumDomainEvents Type { get; }
    }
}
