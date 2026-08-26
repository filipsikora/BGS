using Catan.Core.DomainEvents;
using Catan.Core.Interfaces;
using Catan.Core.Results;
using Catan.Core.Rules;
using Catan.Core.Runtime;
using Catan.Shared.Data;

namespace Catan.Core.UseCases
{
    public sealed class PlayDevCardLogic : BaseUseCase
    {
        public PlayDevCardLogic(GameSession session) : base(session) { }

        public ResultPlayDevCard Handle(int cardId, int playerId)
        {
            var player = Session.GetPlayerById(playerId);
            var card = Session.GetDevCardById(cardId);
            var afterRoll = Session.GetAfterRoll();
            var validation = RulesDevCards.CanPlayDevCard(player, card, afterRoll);
            var nextPhase = EnumGamePhases.NormalRound;
            bool victoryCardPlayed = false;
            bool knightCardPlayed = false;

            if (!validation.Success)
            {
                return ResultPlayDevCard.Fail(validation.Reason, player.ID);
            }

            switch (card.Type)
            {
                case EnumDevelopmentCardTypes.Knight:
                    nextPhase = EnumGamePhases.RobberPlacing;
                    knightCardPlayed = true;
                    break;

                case EnumDevelopmentCardTypes.Monopoly:
                    nextPhase = EnumGamePhases.MonopolyCard;
                    break;

                case EnumDevelopmentCardTypes.RoadBuilding:
                    validation = RulesDevCards.CanPlayRoadBuilding(player);

                    if (!validation.Success)
                        return ResultPlayDevCard.Fail(validation.Reason, player.ID);


                    var roadsAvailable = Math.Min(Session.GetPlayersRoadsLeftById(playerId), 2);

                    Session.CreateRoadBuildingContext(roadsAvailable);

                    nextPhase = EnumGamePhases.RoadBuilding;
                    break;

                case EnumDevelopmentCardTypes.VictoryPoint:
                    victoryCardPlayed = true;
                    break;

                case EnumDevelopmentCardTypes.YearOfPlenty:
                    validation = RulesDevCards.CanPlayYearOfPlenty(Session.GetBank(), 2);

                    if (!validation.Success)
                        return ResultPlayDevCard.Fail(validation.Reason, player.ID);
                     

                    nextPhase = EnumGamePhases.YearOfPlentyCard;
                    break;
            }

            var knightChampionshipUpdateResult = Session.DevCardPlayedMutation(card, player);

            var result = ResultPlayDevCard.Ok(player.ID, card.ID, card.Type, nextPhase);

            result.AddDomainEvent(new DevCardUsedEvent(player.ID, card.ID, card.Type, player.DevelopmentCardsByID.Count, Session.GetPlayerDevCardsByIdData(playerId).ToList()));

            if (victoryCardPlayed)
                result.AddDomainEvent(new VictoryCardUsedEvent(playerId, player.ExtraPoints, player.VictoryPointsCardsUsed));

            if (knightCardPlayed)
                result.AddDomainEvent(new KnightCardUsedEvent(playerId, player.KnightsUsed));

            if (knightChampionshipUpdateResult.Changed)
                result.AddDomainEvent(new KnightChampionChangedEvent(knightChampionshipUpdateResult.OldChampion?.ID, knightChampionshipUpdateResult.NewChampion?.ID,
                    knightChampionshipUpdateResult.OldChampion?.ExtraPoints, knightChampionshipUpdateResult.NewChampion?.ExtraPoints, knightChampionshipUpdateResult.OldChampion?.Points,
                    knightChampionshipUpdateResult.NewChampion?.Points));

            return ApplyPhase(result);
        }
    }
}