using Catan.Core.Interfaces;
using Catan.Shared.Data;

namespace Catan.Core.Results
{
    public abstract class ResultBase
    {
        public bool Success { get; }
        public EnumGamePhases? NextPhase { get; }
        public List<IDomainEvent> DomainEvents { get; } = new();
        
        protected ResultBase(bool success, EnumGamePhases? nextPhase)
        {
            Success = success;
            NextPhase = nextPhase;
        }

        public ResultBase AddDomainEvent(IDomainEvent e)
        {
            DomainEvents.Add(e);
            return this;
        }
    }
}