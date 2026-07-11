#nullable enable
using Catan.Application.Controllers;
using Catan.Application.Interfaces;
using Catan.Application.Phases;
using Catan.Core.DomainEvents;
using Catan.Core.Snapshots.Persistence;
using Catan.Shared.Data;

namespace Catan.Application
{
    public class GameApplication
    {
        public BasePhase Current { get; set; }
        public Facade Facade { get; }

        public GameApplication(Facade facade)
        {
            Facade = facade;
            Current = CreateApplicationPhase(EnumGamePhases.FirstRoundsBuilding);
            Current.Enter();
        }

        public BasePhase CreatePhase(EnumGamePhases phase) => CreateApplicationPhase(phase); // for tests

        public GameResult Execute(ICommand command, int playerId)
        {
            var validation = Current.ValidatePlayer(playerId);

            if (validation.Success != true)
                return validation;

            var result = Current.Handle(command, playerId);

            if (result.NextPhase != null)
            {
                var nextPhase = result.NextPhase.Value;
                Current = CreateApplicationPhase(nextPhase);
                IUIMessages uiMessage = Current.Enter();

                if (uiMessage != null)
                {
                    result.AddUIMessage(uiMessage);
                }

                result.AddDomainEvent(new PhaseChangedEvent(result.NextPhase.Value, Facade.GetPlayersToMove()));
            }

            var uiMessages = Helpers.Mappers.MapDomainEventToUiMessageList(result.DomainEvents);
            result.AddUIMessagesList(uiMessages);

            return result;
        }

        private BasePhase CreateApplicationPhase(EnumGamePhases phase)
        {
            return phase switch
            {
                EnumGamePhases.BeforeRoll => new BeforeRollPhase(Facade),
                EnumGamePhases.FirstRoundsBuilding => new FirstRoundsBuildingPhase(Facade),
                EnumGamePhases.BankTrade => new BankTradePhase(Facade),
                EnumGamePhases.CardDiscarding => new CardDiscardingPhase(Facade),
                EnumGamePhases.CardStealing => new CardStealingPhase(Facade),
                EnumGamePhases.RobberPlacing => new RobberPlacingPhase(Facade),
                EnumGamePhases.DevelopmentCards => new DevelopmentCardsPhase(Facade),
                EnumGamePhases.MonopolyCard => new MonopolyCardPhase(Facade),
                EnumGamePhases.NormalRound => new NormalRoundPhase(Facade),
                EnumGamePhases.RoadBuilding => new RoadBuildingPhase(Facade),
                EnumGamePhases.TradeOffer => new TradeOfferPhase(Facade),
                EnumGamePhases.TradeRequest => new TradeRequestPhase(Facade),
                EnumGamePhases.YearOfPlentyCard => new YearOfPlentyCardPhase(Facade)
            };
        }
        
        public GameStateSnapshot GetGameStateData() => Facade.GetGameStateData();
        public GameStatePerPlayerSnapshot GetGameStatePerPlayerSnapshot(int playerId) => Facade.GetGameStatePerPlayerData(playerId);
        public IEnumerable<int> GetIdsList() => Facade.GetIdsList();

        public void SetPlayerName(string playerName, int playerId) => Facade.SetPlayerName(playerName, playerId);
    }
}