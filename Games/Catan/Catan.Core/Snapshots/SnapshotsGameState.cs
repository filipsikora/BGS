using Catan.Core.Data;
using Catan.Shared.Data;

namespace Catan.Core.Snapshots
{
    public sealed class GameStateSnapshot
    {
        public FullBoardSnapshot Board;
        public FullGameFlowSnapshot GameFlow;

        public List<FullPlayerSnapshot> Players;

        public FullBankSnapshot Bank;
        public FullPhaseContextSnapshot PhaseContext;

        public GameStateSnapshot(FullBoardSnapshot board, FullGameFlowSnapshot gameFlow, List<FullPlayerSnapshot> players, FullBankSnapshot bank, FullPhaseContextSnapshot phaseContext)
        {
            Board = board;
            GameFlow = gameFlow;
            Players = players;
            Bank = bank;
            PhaseContext = phaseContext;
        }
    }

    public sealed class GameStatePerPlayerSnapshot
    {
        public FullBoardSnapshot Board;
        public FullGameFlowSnapshot GameFlow;

        public PlayerResourcesSnapshot Resources;
        public PlayerDataSnapshot Data;

        public GameStatePerPlayerSnapshot(FullBoardSnapshot board, FullGameFlowSnapshot gameFlow, PlayerResourcesSnapshot resources, PlayerDataSnapshot data)
        {
            Board = board;
            GameFlow = gameFlow;
            Resources = resources;
            Data = data;
        }
    }

    public sealed class FullPhaseContextSnapshot
    {
        public TradeOfferContextSnapshot? TradeOffer;
        public TradeRequestContextSnapshot? TradeDraft;
        public RoadBuildingContextSnapshot? RoadBuilding;
        public CardDiscardContextSnapshot? CardDiscarding;
        public CardStealingContextSnapshot? CardStealing;

        public FullPhaseContextSnapshot(TradeOfferContextSnapshot? tradeOffer, TradeRequestContextSnapshot? tradeDraft, RoadBuildingContextSnapshot? roadBuilding, CardDiscardContextSnapshot? cardDiscarding, CardStealingContextSnapshot? cardStealing)
        {
            TradeOffer = tradeOffer;
            TradeDraft = tradeDraft;
            RoadBuilding = roadBuilding;
            CardDiscarding = cardDiscarding;
            CardStealing = cardStealing;
        }
    }

    public sealed class FullPlayerSnapshot
    {
        public PlayerResourcesSnapshot Resources;
        public FullPlayerDataSnapshot Data;

        public FullPlayerSnapshot(PlayerResourcesSnapshot resources, FullPlayerDataSnapshot data)
        {
            Resources = resources;
            Data = data;
        }
    }

    public sealed class FullPlayerDataSnapshot
    {
        public string Name;

        public Dictionary<string, int> BuildingsLeft;

        public int Points;
        public int Knights;
        public int VictoryPoints;
        public int ExtraPoints;

        public List<DevelopmentCardSnapshot> DevCards;

        public FullPlayerDataSnapshot(string name, Dictionary<string, int> buildingsLeft, int points, int knights, int victoryPoints, int extraPoints, List<DevelopmentCardSnapshot> devCards)
        {
            Name = name;
            BuildingsLeft = buildingsLeft;
            Points = points;
            Knights = knights;
            VictoryPoints = victoryPoints;
            ExtraPoints = extraPoints;
            DevCards = devCards;
        }
    }

    public sealed class FullGameFlowSnapshot
    {
        public int TurnNumber;
        public int? RolledNumber;
        public int CurrentPlayerId;

        public int? KnightChampionId;
        public int? RoadChampionId;

        public EnumGamePhases CurrentPhase;

        public FullGameFlowSnapshot(int turnNumber, int? rolledNumber, int currentPlayerId, int? knightChampionId, int? roadChampionId, EnumGamePhases currentPhase)
        {
            TurnNumber = turnNumber;
            RolledNumber = rolledNumber;
            CurrentPlayerId = currentPlayerId;
            KnightChampionId = knightChampionId;
            RoadChampionId = roadChampionId;
            CurrentPhase = currentPhase;
        }
    }

    public sealed class FullBankSnapshot
    {
        public Dictionary<EnumResourceType, int> BankState;
        public List<DevelopmentCardSnapshot> DevCards;

        public FullBankSnapshot(Dictionary<EnumResourceType, int> bankState, List<DevelopmentCardSnapshot> devCards)
        {
            BankState = bankState;
            DevCards = devCards;
        }
    }

    public sealed class FullBoardSnapshot
    {
        public List<FullVertexSnapshot> Vertices;
        public List<FullEdgeSnapshot> Edges;
        public List<HexSnapshot> Hexes;
        public List<PortSnapshot> Ports;
        public int? BlockedHexId;

        public FullBoardSnapshot(List<FullVertexSnapshot> vertices, List<FullEdgeSnapshot> edges, List<HexSnapshot> hexes, List<PortSnapshot> ports, int? blockedHexId)
        {
            Vertices = vertices;
            Edges = edges;
            Hexes = hexes;
            Ports = ports;
            BlockedHexId = blockedHexId;
        }
    }

    public sealed class FullVertexSnapshot
    {
        public int VertexId;
        public List<(int HexQ, int HexR, int CornerIndex)> Corners;

        public int? OwnerId;
        public EnumBuildings Building;

        public FullVertexSnapshot(int vertexId, List<(int HexQ, int HexR, int CornerIndex)> corners, int? ownerId, EnumBuildings building)
        {
            VertexId = vertexId;
            Corners = corners;
            OwnerId = ownerId;
            Building = building;
        }
    }

    public sealed class FullEdgeSnapshot
    {
        public int EdgeId;
        public int VertexAId;
        public int VertexBId;

        public int? OwnerId;

        public FullEdgeSnapshot(int edgeId, int vertexAId, int vertexBId, int? ownerId)
        {
            EdgeId = edgeId;
            VertexAId = vertexAId;
            VertexBId = vertexBId;
            OwnerId = ownerId;
        }
    }
}
