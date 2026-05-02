using Catan.Application.Controllers;
using Catan.Application.UIMessages;
using Catan.Core.Models;
using Catan.Application.Commands;
using Catan.Shared.Data;

namespace Catan.Application.Phases
{
    public class NormalRoundPhase : BasePhase
    {
        private readonly ResourceCostOrStock _selected = new();

        public NormalRoundPhase(Facade facade) : base(facade) { }

        public override GameResult Handle(object command)
        {
            switch (command)
            {
                case ResourceCardSelectedCommand c:
                    return HandleResourceSelectionChanged(c);

                case VertexClickedCommand c:
                    return HandleVertexClicked(c);

                case EdgeClickedCommand c:
                    return HandleEdgeClicked(c);

                case BuildVillageCommand c:
                    return HandleVillageRequested(c);

                case BuildRoadCommand c:
                    return HandleRoadRequested(c);

                case UpgradeVillageCommand c:
                    return HandleTownRequested(c);

                case BankTradeCommand c:
                    return GameResult.Ok(EnumGamePhases.BankTrade);

                case OfferTradeCommand c:
                    return HandleTradeRequested(c);

                case EndTurnCommand c:
                    return HandleEndTurnRequested(c);

                case BuyDevelopmentCardCommand c:
                    return HandleDevelopmentCardsBuyRequested(c);

                case ShowDevelopmentCardsCommand c:
                    return GameResult.Ok(EnumGamePhases.DevelopmentCards);

                default:
                    return GameResult.Fail();
            }
        }

        private GameResult HandleResourceSelectionChanged(ResourceCardSelectedCommand signal)
        {
            if (signal.IsSelected)
            {
                _selected.AddExactAmount(signal.Type, 1);
            }

            else
            {
                _selected.SubtractExactAmount(signal.Type, 1);
            }

            bool canTrade = _selected.Total() > 0;

            return GameResult.Ok().AddUIMessage(new SelectionChangedMessage(canTrade));
        }

        private GameResult HandleVertexClicked(VertexClickedCommand signal)
        {
            var (village, road, town) = Facade.GetVertexBuildOptions(signal.VertexId, Facade.GetCurrentPlayerId());

            return GameResult.Ok().AddUIMessage(new VertexHighlightedMessage(signal.VertexId)).AddUIMessage(new BuildOptionsSentMessage(village, road, town));
        }

        private GameResult HandleEdgeClicked(EdgeClickedCommand signal)
        {
            var (village, road, town) = Facade.GetEdgeBuildOptions(signal.EdgeId);

            return GameResult.Ok().AddUIMessage(new EdgeHighlightedMessage(signal.EdgeId)).AddUIMessage(new BuildOptionsSentMessage(village, road, town));
        }

        private GameResult HandleVillageRequested(BuildVillageCommand signal)
        {
            var result = Facade.UseBuildVillage(signal.VertexId);

            if (!result.Success)
            {
                return GameResult.Fail().AddUIMessage(new ActionRejectedMessage(result.PlayerId, result.Reason));
            }

            return GameResult.Ok().AddDomainEventsList(result.DomainEvents);
        }

        private GameResult HandleRoadRequested(BuildRoadCommand signal)
        {
            var result = Facade.UseBuildRoad(signal.EdgeId);

            if (!result.Success)
            {
                return GameResult.Fail().AddUIMessage(new ActionRejectedMessage(result.PlayerId, result.Reason));
            }

            return GameResult.Ok().AddDomainEventsList(result.DomainEvents);
        }

        private GameResult HandleTownRequested(UpgradeVillageCommand signal)
        {
            var result = Facade.UseUpgradeVillage(signal.VertexId);

            if (!result.Success)
            {
                return GameResult.Fail().AddUIMessage(new ActionRejectedMessage(result.PlayerId, result.Reason));
            }

            return GameResult.Ok().AddDomainEventsList(result.DomainEvents);
        }

        private GameResult HandleTradeRequested(OfferTradeCommand signal)
        {
            int playerId = Facade.GetCurrentPlayerId();

            if (_selected.Total() == 0)
                return GameResult.Fail().AddUIMessage(new ActionRejectedMessage(playerId, ConditionFailureReason.InvalidSelection));

            var result = Facade.UsePrepareTrade(_selected);

            if (!result.Success)
            {
                return GameResult.Fail().AddUIMessage(new ActionRejectedMessage(playerId, result.Reason));
            }

            return GameResult.Ok(result.NextPhase);
        }

        private GameResult HandleEndTurnRequested(EndTurnCommand signal)
        {
            var result = Facade.UseFinishTurn();

            return GameResult.Ok(result.NextPhase).AddDomainEventsList(result.DomainEvents);
        }

        private GameResult HandleDevelopmentCardsBuyRequested(BuyDevelopmentCardCommand signal)
        {
            var result = Facade.UseBuyDevCard();

            if (!result.Success)
            {
                return GameResult.Fail().AddUIMessage(new ActionRejectedMessage(result.PlayerId, result.Reason));
            }

            return GameResult.Ok().AddDomainEventsList(result.DomainEvents);
        }
    }
}