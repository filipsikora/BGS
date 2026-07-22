#nullable enable
using Catan.Application.Controllers;
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

        public override GameResult Handle(object command, int playerId)
        {
            switch (command)
            {
                case VertexClickedCommand c:
                    return HandleVertexClicked(c, playerId);

                case EdgeClickedCommand c:
                    return HandleEdgeClicked(c, playerId);

                case BuildVillageCommand c:
                    return HandleBuildVillage(c, playerId);

                case BuildRoadCommand c:
                    return HandleBuildRoad(c, playerId);

                case EndTurnCommand c:
                    return HandleTurnEnded(c);

                case StartGameCommand c:
                    return GameResult.Ok(EnumGamePhases.FirstRoundsBuilding); // not used?

                default:
                    return GameResult.Fail();
            }
        }

        private GameResult HandleVertexClicked(VertexClickedCommand signal, int playerId)
        { 
            if (villagePlaced && !roadPlaced)
                return GameResult.Fail().AddUIMessage(new ActionRejectedMessage(playerId, ConditionFailureReason.InitialVillageBuilt));

            if (villagePlaced && roadPlaced)
                return GameResult.Fail().AddUIMessage(new ActionRejectedMessage(playerId, ConditionFailureReason.FreeBuildingDone));

            var (village, road, town) = Facade.GetVertexBuildOptions(signal.VertexId, playerId);

            return GameResult.Ok().AddUIMessage(new VertexHighlightedMessage(signal.VertexId)).AddUIMessage(new BuildOptionsSentMessage(village, road, town));
        }
        
        private GameResult HandleEdgeClicked(EdgeClickedCommand signal, int playerId)
        {
            if (!villagePlaced)
                return GameResult.Fail().AddUIMessage(new ActionRejectedMessage(playerId, ConditionFailureReason.InitialVillageNotBuilt));

            if (roadPlaced)
                return GameResult.Fail().AddUIMessage(new ActionRejectedMessage(playerId, ConditionFailureReason.FreeBuildingDone));

            var (village, road, town) = Facade.GetEdgeBuildOptions(signal.EdgeId);

            return GameResult.Ok().AddUIMessage(new EdgeHighlightedMessage(signal.EdgeId)).AddUIMessage(new BuildOptionsSentMessage(village, road, town));  
        }

        private GameResult HandleBuildVillage(BuildVillageCommand signal, int playerId)
        {
            var result = Facade.UseBuildInitialVillage(signal.VertexId, playerId);

            if (!result.Success)
            {
                return GameResult.Fail().AddUIMessage(new ActionRejectedMessage(result.PlayerId, result.Reason));
            }

            villagePlaced = true;

            return GameResult.Ok().AddDomainEventsList(result.DomainEvents);
        }

        private GameResult HandleBuildRoad(BuildRoadCommand signal, int playerId)
        {
            var vertexId = Facade.GetLastPlacedVillagePositionId();
            var result = Facade.UseBuildInitialRoad(signal.EdgeId, vertexId, playerId);

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