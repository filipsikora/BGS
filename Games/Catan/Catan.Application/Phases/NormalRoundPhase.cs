using Catan.Application.Controllers;
using Catan.Application.UIMessages;
using Catan.Application.Commands;
using Catan.Shared.Data;

namespace Catan.Application.Phases
{
    public class NormalRoundPhase : BasePhase
    {
        public NormalRoundPhase(Facade facade) : base(facade) { }

        public override GameResult Handle(object command, int playerId)
        {
            switch (command)
            {
                case VertexClickedCommand c:
                    return HandleVertexClicked(c, playerId);

                case EdgeClickedCommand c:
                    return HandleEdgeClicked(c);

                case BuildVillageCommand c:
                    return HandleVillageRequested(c, playerId);

                case BuildRoadCommand c:
                    return HandleRoadRequested(c, playerId);

                case UpgradeVillageCommand c:
                    return HandleTownRequested(c, playerId);

                case BankTradeCommand c:
                    return GameResult.Ok(EnumGamePhases.BankTrade);

                case CardsSelectedCommand c:
                    return HandleTradeRequested(c, playerId);

                case EndTurnCommand c:
                    return HandleEndTurnRequested(c, playerId);

                case BuyDevelopmentCardCommand c:
                    return HandleDevelopmentCardsBuyRequested(c, playerId);

                case ShowDevelopmentCardsCommand c:
                    return GameResult.Ok(EnumGamePhases.DevelopmentCards);

                default:
                    return GameResult.Fail();
            }
        }

        private GameResult HandleVertexClicked(VertexClickedCommand signal, int playerId)
        {
            var (village, road, town) = Facade.GetVertexBuildOptions(signal.VertexId, playerId));

            return GameResult.Ok().AddUIMessage(new VertexHighlightedMessage(signal.VertexId)).AddUIMessage(new BuildOptionsSentMessage(village, road, town));
        }

        private GameResult HandleEdgeClicked(EdgeClickedCommand signal)
        {
            var (village, road, town) = Facade.GetEdgeBuildOptions(signal.EdgeId);

            return GameResult.Ok().AddUIMessage(new EdgeHighlightedMessage(signal.EdgeId)).AddUIMessage(new BuildOptionsSentMessage(village, road, town));
        }

        private GameResult HandleVillageRequested(BuildVillageCommand signal, int playerId)
        {
            var result = Facade.UseBuildVillage(signal.VertexId, playerId);

            if (!result.Success)
            {
                return GameResult.Fail().AddUIMessage(new ActionRejectedMessage(result.PlayerId, result.Reason));
            }

            return GameResult.Ok().AddDomainEventsList(result.DomainEvents);
        }

        private GameResult HandleRoadRequested(BuildRoadCommand signal, int playerId)
        {
            var result = Facade.UseBuildRoad(signal.EdgeId, playerId);

            if (!result.Success)
            {
                return GameResult.Fail().AddUIMessage(new ActionRejectedMessage(result.PlayerId, result.Reason));
            }

            return GameResult.Ok().AddDomainEventsList(result.DomainEvents);
        }

        private GameResult HandleTownRequested(UpgradeVillageCommand signal, int playerId)
        {
            var result = Facade.UseUpgradeVillage(signal.VertexId, playerId);

            if (!result.Success)
            {
                return GameResult.Fail().AddUIMessage(new ActionRejectedMessage(result.PlayerId, result.Reason));
            }

            return GameResult.Ok().AddDomainEventsList(result.DomainEvents);
        }

        private GameResult HandleTradeRequested(CardsSelectedCommand signal, int playerId)
        {
            if (signal.Resources.Total() == 0)
                return GameResult.Fail().AddUIMessage(new ActionRejectedMessage(playerId, ConditionFailureReason.InvalidSelection));

            var result = Facade.UsePrepareTrade(signal.Resources, playerId);

            if (!result.Success)
            {
                return GameResult.Fail().AddUIMessage(new ActionRejectedMessage(playerId, result.Reason));
            }

            return GameResult.Ok(result.NextPhase);
        }

        private GameResult HandleEndTurnRequested(EndTurnCommand signal, int playerId)
        {
            var result = Facade.UseFinishTurn(playerId);

            return GameResult.Ok(result.NextPhase).AddDomainEventsList(result.DomainEvents);
        }

        private GameResult HandleDevelopmentCardsBuyRequested(BuyDevelopmentCardCommand signal, int playerId)
        {
            var result = Facade.UseBuyDevCard(playerId);

            if (!result.Success)
            {
                return GameResult.Fail().AddUIMessage(new ActionRejectedMessage(result.PlayerId, result.Reason));
            }

            return GameResult.Ok().AddDomainEventsList(result.DomainEvents);
        }
    }
}