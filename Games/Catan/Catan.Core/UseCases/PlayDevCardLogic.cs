using Catan.Core.DomainEvents;
using Catan.Core.Results;
using Catan.Core.Rules;
using Catan.Shared.Data;

namespace Catan.Core.UseCases
{
    public sealed class PlayDevCardLogic : BaseUseCase
    {
        public PlayDevCardLogic(GameSession session) : base(session) { }

        public ResultPlayDevCard Handle(int cardId)
        {
            var player = Session.GetCurrentPlayer();
            var card = Session.GetDevCardById(cardId);
            var afterRoll = Session.GetAfterRoll();
            var validation = RulesDevCards.CanPlayDevCard(player, card, afterRoll);
            var nextPhase = EnumGamePhases.NormalRound;

            if (!validation.Success)
            {
                return ResultPlayDevCard.Fail(validation.Reason, player.ID);
            }

            switch (card.Type)
            {
                case EnumDevelopmentCardTypes.Knight:
                    nextPhase = EnumGamePhases.RobberPlacing;
                    break;

                case EnumDevelopmentCardTypes.Monopoly:
                    nextPhase = EnumGamePhases.MonopolyCard;
                    break;

                case EnumDevelopmentCardTypes.RoadBuilding:
                    validation = RulesDevCards.CanPlayRoadBuilding(player);

                    if (!validation.Success)
                        return ResultPlayDevCard.Fail(validation.Reason, player.ID);


                    var roadsAvailable = Math.Min(Session.GetCurrentPlayersRoadsLeft(), 2);

                    Session.CreateRoadBuildingContext(roadsAvailable);

                    nextPhase = EnumGamePhases.RoadBuilding;
                    break;

                case EnumDevelopmentCardTypes.VictoryPoint:
                    break;

                case EnumDevelopmentCardTypes.YearOfPlenty:
                    validation = RulesDevCards.CanPlayYearOfPlenty(Session.GetBank(), 2);

                    if (!validation.Success)
                        return ResultPlayDevCard.Fail(validation.Reason, player.ID);


                    nextPhase = EnumGamePhases.YearOfPlentyCard;
                    break;
            }

            Session.DevCardPlayedMutation(card);

            var result = ResultPlayDevCard.Ok(player.ID, card.ID, card.Type, nextPhase);
            result.AddDomainEvent(new PlayerStateChangedEvent(player.ID));

            return ApplyPhase(result);
        }
    }
}