using BGS.Shared.Dtos;
using Catan.Backend.GameManagement;
using Catan.Core.DomainEvents;
using Catan.Core.Interfaces;
using Newtonsoft.Json.Linq;

namespace Catan.Backend.Helpers
{
    public class DomainEventsDispatcher
    {
        public List<GameUpdateDto> Dispatch(IDomainEvent domainEvent, CatanGameInstance game)
        {
            var type = domainEvent.Type.ToString();

            return domainEvent switch
            {
                BankTradeDoneEvent e => DispatchBankTradeDoneEvent(e, game, type),
                _ => throw new NotSupportedException($"Unknown domain event: {type}")
            };
        }

        private List<GameUpdateDto> DispatchBankTradeDoneEvent(BankTradeDoneEvent domainEvent, CatanGameInstance game, string type)
        {
            var payload = JToken.FromObject(domainEvent);

            var updatesList = new List<GameUpdateDto>();

            foreach (var entry in game.PlayerTokens)
            {
                updatesList.Add(new GameUpdateDto(type, entry.Key, payload));
            }

            return updatesList;
        }
    }
}