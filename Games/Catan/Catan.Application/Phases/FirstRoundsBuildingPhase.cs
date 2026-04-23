#nullable enable
using Catan.Application.Controllers;
using Catan.Application.Interfaces;
using Catan.Application.UIMessages;
using Catan.Application.Commands;
using Catan.Shared.Data;

namespace Catan.Application.Phases
{
    public class FirstRoundsBuildingPhase : BasePhase
    {
        private bool villagePlaced = false;
        private bool roadPlaced = false;

        public FirstRoundsBuildingPhase(Facade facade) : base(facade) { }

        public override IUIMessages? Enter()
        {
            return new LogMessageMessage(EnumLogTypes.Info, "Select a vertex to build your free village, then select an edge to build a free road", 4);
        }

        public override GameResult Handle(object command)
        {
            switch (command)
            {
                case VertexClickedCommand c:
                    return HandleVertexClicked(c);

                case EdgeClickedCommand c:
                    return HandleEdgeClicked(c);

                case BuildVillageCommand c:
                    return HandleBuildVillage(c);

                case BuildRoadCommand c:
                    return HandleBuildRoad(c);

                case EndTurnCommand c:
                    return HandleTurnEnded(c);

                case StartGameCommand c:
                    return GameResult.Ok(EnumGamePhases.FirstRoundsBuilding); // not used?

                default:
                    return GameResult.Fail();
            }
        }

        private GameResult HandleVertexClicked(VertexClickedCommand signal)
        { 
            if (villagePlaced)
                return GameResult.Fail().AddUIMessage(new LogMessageMessage(EnumLogTypes.Info, "Build a road now"));

            var (village, road, town) = Facade.GetVertexBuildOptions(signal.VertexId, Facade.GetCurrentPlayerId());

            return GameResult.Ok().AddUIMessage(new VertexHighlightedMessage(signal.VertexId)).AddUIMessage(new BuildOptionsSentMessage(village, road, town));
        }

        private GameResult HandleEdgeClicked(EdgeClickedCommand signal)
        {
            if (!villagePlaced)
                return GameResult.Fail().AddUIMessage(new LogMessageMessage(EnumLogTypes.Info, "Build a village first"));

            if (roadPlaced)
                return GameResult.Fail().AddUIMessage(new LogMessageMessage(EnumLogTypes.Info, "Finish turn now"));

            var (village, road, town) = Facade.GetEdgeBuildOptions(signal.EdgeId);

            return GameResult.Ok().AddUIMessage(new EdgeHighlightedMessage(signal.EdgeId)).AddUIMessage(new BuildOptionsSentMessage(village, road, town));
        }

        private GameResult HandleBuildVillage(BuildVillageCommand signal)
        {
            var result = Facade.UseBuildInitialVillage(signal.VertexId);

            if (!result.Success)
            {
                return GameResult.Fail().AddUIMessage(new ActionRejectedMessage(result.PlayerId, result.Reason));
            }

            villagePlaced = true;

            return GameResult.Ok().AddDomainEventsList(result.DomainEvents);
        }

        private GameResult HandleBuildRoad(BuildRoadCommand signal)
        {
            var vertexId = Facade.GetLastPlacedVillagePositionId();
            var result = Facade.UseBuildInitialRoad(signal.EdgeId, vertexId);

            if (!result.Success)
            {
                return GameResult.Fail().AddUIMessage(new ActionRejectedMessage(result.PlayerId, result.Reason));
            }
            
            roadPlaced = true;

            return GameResult.Ok().AddDomainEventsList(result.DomainEvents);
        }

        private GameResult HandleTurnEnded(EndTurnCommand signal)
        {
            var result = Facade.UseFinishTurn();

            if (!result.Success)
            {
                return GameResult.Fail().AddUIMessage(new ActionRejectedMessage(result.NewCurrentPlayerId, result.Reason));
            }

            return GameResult.Ok(result.NextPhase).AddDomainEventsList(result.DomainEvents);
        }
    }
}