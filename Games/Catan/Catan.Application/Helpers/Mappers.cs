using Catan.Application.Interfaces;
using Catan.Application.UIMessages;
using Catan.Core.DomainEvents;
using Catan.Core.Interfaces;

namespace Catan.Application.Helpers
{
    public static class Mappers
    {
        public static List<IUIMessages> MapDomainEventToUiMessageList(IEnumerable<IDomainEvent> domainEvents)
        {
            var uiMessages = new List<IUIMessages>();

            foreach (var domainEvent in domainEvents)
            {
                var uiMessage = MapDomainEventToUiMessage(domainEvent);
                uiMessages.Add(uiMessage);
            }

            return uiMessages;
        }
        
        public static IUIMessages MapDomainEventToUiMessage(IDomainEvent domainEvent)
        {
            return domainEvent switch
            {
                VillagePlacedEvent e => new VillagePlacedMessage(e.VertexId, e.OwnerId),
                RoadPlacedEvent e => new RoadPlacedMessage(e.EdgeId, e.OwnerId),
                TownPlacedEvent e => new TownPlacedMessage(e.VertexId, e.OwnerId),
                DevelopmentCardBoughtEvent e => new DevelopmentCardBoughtMessage(e.CardId),
                RobberPlacedEvent e => new RobberPlacedMessage(e.HexId),
                PlayerStateChangedEvent e => new PlayerStateChangedMessage(e.PlayerId),
                TurnNumberChangedEvent e => new TurnNumberChangedMessage(e.NewTurnNumber),
                RolledNumberChangedEvent e => new DiceRollChangedMessage(e.NewRolledNumber),
                _ => throw new Exception($"Unknown domain event: {domainEvent}")
            };
        }
    }
}