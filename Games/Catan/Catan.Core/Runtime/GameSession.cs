using Catan.Core.Conditions;
using Catan.Core.Data;
using Catan.Core.Engine;
using Catan.Core.Models;
using Catan.Core.UseCases;
using Catan.Core.Results;
using Catan.Core.Rules;
using Catan.Shared.Data;
using Catan.Core.Snapshots.ClientQueries;
using Catan.Core.Queries.GameStateSnapshotBuilders;
using Catan.Core.Snapshots.Persistence;

namespace Catan.Core.Runtime
{
    public class GameSession
    {
        private readonly GameState _game;

        private readonly BoardStateReader _boardReader;
        private readonly PlayersStateReader _playersReader;
        private readonly ThiefStateReader _thiefReader;
        private readonly TradeStateReader _tradeReader;
        private readonly GameFlowStateReader _gameFlowReader;
        private readonly DevCardsStateReader _devCardsReader;

        private readonly GameStateSnapshotBuilder GameStateSnapshotBuilder;

        private readonly BankTradeLogic _bankTrade;
        private readonly BlockHexLogic _blockHex;
        private readonly SelectVictimLogic _selectVictim;
        private readonly BuildFreeRoadLogic _buildFreeRoad;
        private readonly BuildInitialRoadLogic _buildInitialRoad;
        private readonly BuildInitialVillageLogic _buildInitialVillage;
        private readonly BuildRoadLogic _buildRoad;
        private readonly BuildVillageLogic _buildVillage;
        private readonly BuyDevCardLogic _buyDevCard;
        private readonly DiscardCardsLogic _discardCards;
        private readonly FinishTurnLogic _finishTurn;
        private readonly OfferTradeLogic _offerTrade;
        private readonly PlayDevCardLogic _playDevCard;
        private readonly ReactToTradeLogic _reactToTrade;
        private readonly RollDiceLogic _rollDice;
        private readonly StealCardLogic _stealCard;
        private readonly UpgradeVillageLogic _upgradeVillage;
        private readonly UseMonopolyLogic _useMonopoly;
        private readonly UseYearOfPlentyLogic _useYearOfPlenty;
        private readonly PrepareTradeOfferLogic _prepareTrade;

        public GameSession(GameState game)
        {
            _game = game;

            _boardReader = new BoardStateReader();
            _playersReader = new PlayersStateReader();
            _thiefReader = new ThiefStateReader();
            _tradeReader = new TradeStateReader();
            _gameFlowReader = new GameFlowStateReader();
            _devCardsReader = new DevCardsStateReader();

            GameStateSnapshotBuilder = new GameStateSnapshotBuilder(this);

            _bankTrade = new BankTradeLogic(this);
            _blockHex = new BlockHexLogic(this);
            _selectVictim = new SelectVictimLogic(this);
            _buildFreeRoad = new BuildFreeRoadLogic(this);
            _buildInitialRoad = new BuildInitialRoadLogic(this);
            _buildInitialVillage = new BuildInitialVillageLogic(this);
            _buildRoad = new BuildRoadLogic(this);
            _buildVillage = new BuildVillageLogic(this);
            _buyDevCard = new BuyDevCardLogic(this);
            _discardCards = new DiscardCardsLogic(this);
            _finishTurn = new FinishTurnLogic(this);
            _offerTrade = new OfferTradeLogic(this);
            _playDevCard = new PlayDevCardLogic(this);
            _reactToTrade = new ReactToTradeLogic(this);
            _rollDice = new RollDiceLogic(this);
            _stealCard = new StealCardLogic(this);
            _upgradeVillage = new UpgradeVillageLogic(this);
            _useMonopoly = new UseMonopolyLogic(this);
            _useYearOfPlenty = new UseYearOfPlentyLogic(this);
            _prepareTrade = new PrepareTradeOfferLogic(this);
        }

        // phase logic //

        public ResultBankTrade UseBankTrade(EnumResourceType offered, EnumResourceType desired) => _bankTrade.Handle(offered, desired);
        public ResultBlockHex UseBlockHex(int hexId) => _blockHex.Handle(hexId);
        public ResultCondition UseSelectVictim(int victimId) => _selectVictim.Handle(victimId);
        public ResultBuildFreeRoad UseBuildFreeRoad(int edgeId) => _buildFreeRoad.Handle(edgeId);
        public ResultBuildInitialRoad UseBuildInitialRoad(int edgeId, int vertexId) => _buildInitialRoad.Handle(edgeId, vertexId);
        public ResultBuildInitialVillage UseBuildInitialVillage(int vertexId) => _buildInitialVillage.Handle(vertexId);
        public ResultBuildRoad UseBuildRoad(int edgeId) => _buildRoad.Handle(edgeId);
        public ResultBuildVillage UseBuildVillage(int vertexId) => _buildVillage.Handle(vertexId);
        public ResultUpgradeVillage UseUpgradeVillage(int vertexId) => _upgradeVillage.Handle(vertexId);
        public ResultRollDice UseRollDice() => _rollDice.Handle();
        public ResultCondition UseDiscard(int discardingPlayerId, ResourceCostOrStock resourcesSelected) => _discardCards.Handle(discardingPlayerId, resourcesSelected);
        public ResultStealResource UseSteal(int victimId, EnumResourceType resource) => _stealCard.Handle(victimId, resource);
        public ResultPlayDevCard UseDevCard(int cardId) => _playDevCard.Handle(cardId);
        public ResultFinishTurn UseFinishTurn() => _finishTurn.Handle();
        public ResultMonopolyCard UseMonopolyCard(EnumResourceType resource) => _useMonopoly.Handle(resource);
        public ResultBuyDevCard UseBuyDevCard() => _buyDevCard.Handle();
        public ResultCondition UsePrepareTrade(ResourceCostOrStock offered) => _prepareTrade.Handle(offered);
        public ResultPlayerTrade UseOfferTrade(int buyerId, ResourceCostOrStock desired) => _offerTrade.Handle(buyerId, desired);
        public ResultPlayerTrade UseReactToTrade() => _reactToTrade.Handle();
        public ResultYearOfPlenty UseYearOfPlenty(ResourceCostOrStock resources) => _useYearOfPlenty.Handle(resources);


        // GETTERS //
        // board //

        internal HexTile GetHexById(int id) => _game.Map.GetHexById(id);
        internal Edge GetEdgeById(int id) => _game.Map.GetEdgeById(id);
        internal Vertex GetVertexById(int id) => _game.Map.GetVertexById(id);
        internal Port GetPortByEdge(Edge edge) => _game.Map.PortList.Find(p => p.Edge == edge);
        internal IEnumerable<HexTile> GetAllHexTilesView()
        {
            foreach (var hex in _game.Map.HexList)
                yield return hex;
        }
        internal IEnumerable<Vertex> GetAllVerticesView()
        {
            foreach (var vertex in _game.Map.VertexList)
                yield return vertex;
        }
        internal IEnumerable<Edge> GetAllEdgesView()
        {
            foreach (var edge in _game.Map.Edges)
                yield return edge;
        }
        internal IEnumerable<Port> GetAllPortsView()
        {
            foreach (var port in _game.Map.PortList)
                yield return port;
        }

        public bool TryGetVertexById(int vertexId) => _game.Map.TryGetVertexById(vertexId);
        public bool TryGetEdgeById(int edgeId) => _game.Map.TryGetEdgeById(edgeId);
        public bool TryGetHexById(int hexId) => _game.Map.TryGetHexById(hexId);
        public int GetDesertHexId() => _game.Map.HexList.Find(h => h.FieldType == EnumFieldTypes.Desert).Id;
        public int GetBlockedHexId() => _game.GetBlockedHexId();
        public List<(int HexQ, int HexR, int CornerIndex)> GetVertexCorners(int vertexId) => _boardReader.GetVertexCorners(GetVertexById(vertexId));
        public VertexSnapshot GetVertexData(int vertexId) => _boardReader.GetVertexData(GetVertexById(vertexId));
        public EdgeSnapshot GetEdgeData(int edgeId) => _boardReader.GetEdgeData(GetEdgeById(edgeId));
        public HexSnapshot GetHexData(int hexId) => _boardReader.GetHexData(GetHexById(hexId));
        public PortSnapshot GetPortData(int edgeId) => _boardReader.GetPortData(GetEdgeById(edgeId), GetPortByEdge(GetEdgeById(edgeId)));
        public List<int> GetAdjacentToHexPlayersIds(int hexId) => _boardReader.GetAdjacentToHexPlayersIds(GetHexById(hexId));

        // players //

        internal Player GetCurrentPlayer() => _game.CurrentPlayer;
        internal Player GetPlayerById(int playerId) => _game.GetPlayerById(playerId);
        internal Player GetPlayerByIndex(int index) => _game.PlayerList[index];
        internal List<Player> GetPlayersByIds(List<int> playersIds) => playersIds.Select(GetPlayerById).ToList();
        internal IEnumerable<Player> GetAllPlayersView()
        {
            foreach (var player in _game.PlayerList)
                yield return player;
        }

        public int GetCurrentPlayerId() => _game.CurrentPlayer.ID;
        public int GetCurrentPlayersRoadsLeft() => _game.CurrentPlayer.BuildingCount(EnumBuildings.Road);
        public int GetCurrentPlayerResourceAmount(EnumResourceType resource) => _game.CurrentPlayer.Resources.Get(resource);
        public bool PlayerHasEnoughResources(int playerAmount, int neededAmount) => ConditionsTrade.PlayerHasEnoughResource(playerAmount, neededAmount).Success;
        public List<PlayerNameSnapshot> GetAllPlayersNamesData() => _playersReader.GetAllPlayersNames(_game.PlayerList);
        public List<PlayerNameSnapshot> GetSomePlayersNamesData(List<int> playersIds) => _playersReader.GetSomePlayersNames(playersIds.Select(GetPlayerById).ToList());
        public List<PlayerNameSnapshot> GetNotCurrentPlayerNamesData() => _playersReader.GetNotCurrentPlayersNames(_game.PlayerList.Where(p => p.ID != GetCurrentPlayerId()));
        public PlayerResourcesSnapshot GetPlayerssResourcesData(int playerId) => _playersReader.GetPlayersCards(GetPlayerById(playerId));
        public PlayerDataSnapshot GetPlayerData(int playerId) => _playersReader.GetPlayersData(GetPlayerById(playerId));
        public PlayerResourcesSnapshot GetVictimCardsData() => _playersReader.GetVictimsCards(GetPlayerById(GetVictimId()));
        public FullPlayerSnapshot GetFullPlayerData(int playerId) => _playersReader.GetFullPlayerData(GetPlayerById(playerId), GetPlayerDevCardsByIdData(playerId));


        // game flow //

        public bool GetAfterRoll() => _game.GetAfterRoll();
        public int GetTurn() => _game.Turn;
        public int GetLastRoll() => _game.LastRoll;
        public int? GetKnightChampionId() => _game.KnightChampion != null ? _game.KnightChampion.ID : null;
        public int? GetRoadChampionId() => _game.RoadChampion != null ? _game.RoadChampion.ID : null;

        // thief //

        internal (bool exists, CardStealingContext context) TryGetCardStealingContext()
        {
            var context = _game.CardStealingProgress;

            return (context != null, context);
        }
        internal (bool exists, CardDiscardContext context) TryGetCardDiscardingContext()
        {
            var context = _game.CardDiscardingProgress;

            return (context != null, context);
        }
        internal CardDiscardContext? GetCardDiscardingContext() => _game.CardDiscardingProgress;
        internal CardStealingContext? GetCardStealingContext() => _game.CardStealingProgress;

        public bool GetPlayersLeftToDiscard() => _thiefReader.GetPlayersLeftToDiscard(_game.PlayerList);
        public bool GetCardDiscardingContextExistance() => _game.CardDiscardingProgress != null;
        public int GetNextToDiscardId() => _game.CardDiscardingProgress.PlayersToDiscard.Peek();
        public void GetPlayersToDiscard() => CreateCardDiscardingContext(_thiefReader.GetCardsDiscardingPlayers(_game.PlayerList).Select(p => p.ID));
        public bool CanPlayerDiscard(ResourceCostOrStock resourcesSelected, int discardingPlayerId) => _thiefReader.CanPlayerDiscard(resourcesSelected, GetPlayerById(discardingPlayerId));
        public int GetVictimId() => _game.CardStealingProgress.VictimId;
        public List<int> GetPossibleVictimsIds() => _thiefReader.GetPossibleVictimsIds(GetPlayersByIds(GetAdjacentToHexPlayersIds(_game.BlockedHexId.Value)), _game.CurrentPlayer);

        // buildings //

        internal RoadBuildingContext? GetRoadBuildingContext() => _game.RoadBuildingProgress;
        public (bool village, bool road, bool town) GetVertexBuildOptions(int vertexId, int playerId)
        {
            var player = GetPlayerById(playerId);

            return (RulesPlacement.CanPlaceVillage(vertexId, this).Success, false, RulesPlacement.CanPlaceTown(player, vertexId, this).Success);
        }
        public (bool village, bool road, bool town) GetEdgeBuildOptions(int edgeId)
        {
            return (false, RulesPlacement.CanPlaceRoad(edgeId, this).Success, false);
        }
        public bool GetRoadsLeftToBuild() => _game.RoadBuildingProgress == null ? false : true;
        public int GetLastPlacedVillagePositionId() => _game.LastPlacedVillagePosition.Id;
        public bool GetVillagePlacedThisTurn() => _game.VillagePlacedThisTurn;
        public bool GetRoadPlacedThisTurn() => _game.RoadPlacedThisTurn;

        // phases //

        public EnumGamePhases GetCurrentCorePhase() => _game.CurrentPhase;
        public bool CheckIfIsCorePhase(EnumGamePhases phase) => _game.CurrentPhase == phase;
        public EnumGamePhases GetNextPhaseFromAfterRoll() => GetAfterRoll() ? EnumGamePhases.NormalRound : EnumGamePhases.BeforeRoll;
        public EnumGamePhases? GetNextPhaseAfterDiscarding() => _game.GetCardDiscardingProgress() == 0 ? EnumGamePhases.RobberPlacing : null;
        public bool CheckIfInitialRoundsRemaining() => _game.FirstRoundsIndices.Count > 0;

        // trade //

        internal ResourceCostOrStock GetOfferedResources() => _game.TradeDraft.Offered;
        internal PlayerTradeContext? TryGetPlayerTradeContext() => _game.LastPlayerTradeOffered;
        internal TradeDraftContext? TryGetTradeDraftContext() => _game.TradeDraft;

        public int GetCurrentPlayerTradeRatio(EnumResourceType resource) => _tradeReader.GetCurrentPlayerTradeRatio(resource, _game.CurrentPlayer, _game.Map.PortList.Find(port => port.Type == resource));

        // resources //

        internal ResourceCostOrStock GetBank() => _game.Bank;

        public ResourcesAvailabilitySnapshot GetResourcesAvailabilityData() => _gameFlowReader.GetResourcesAvailabilityData(GetBank());
        public bool CheckIfCardsSelected(ResourceCostOrStock resources) => resources.Total() > 0;
        public bool CheckIfExactCardsAmountSelected(ResourceCostOrStock resources, int amount) => ConditionsResources.HasExactResourcesNumber(resources, amount).Success;
        public (int, bool) GetNextIndex() => _gameFlowReader.GetNextIndex(_game.FirstRoundsIndices, _game.CurrentPlayerIndex, _game.PlayerList.Count);

        // dev cards //

        internal DevelopmentCard GetFirstDevCard() => _game.DevelopmentCardsDeckAvailable[0];
        internal DevelopmentCard GetDevCardById(int cardId) => _game.GetDevCardById(cardId);
        internal List<DevelopmentCard> GetDevCardsLeft() => _game.DevelopmentCardsDeckAvailable;

        public List<DevelopmentCardSnapshot> GetDevCardsInBankData() => _gameFlowReader.GetDevCardsInBankData(_game.DevelopmentCardsDeckAvailable);
        public IReadOnlyList<DevelopmentCardSnapshot> GetCurrentPlayerDevCardsData() => _devCardsReader.GetCurrentPlayerDevCardsData(_game.CurrentPlayer.DevelopmentCardsByID.Select(id => GetDevCardById(id)).ToList(), GetAfterRoll());
        public IReadOnlyList<DevelopmentCardSnapshot> GetPlayerDevCardsByIdData(int playerId) => _devCardsReader.GetPlayerDevCardsByIdData(GetPlayerById(playerId).DevelopmentCardsByID.Select(id => GetDevCardById(id)).ToList(), GetAfterRoll());

        // GameStateSnapshot //

        public GameStateSnapshot GetGameStateData() => GameStateSnapshotBuilder.GetGameStateData();

        // internal setters //

        internal void BankTradeMutation(EnumResourceType offered, EnumResourceType desired, int ratio) => _game.BankTradeMutation(offered, desired, ratio);
        internal void BlockHexMutation(HexTile hex) => _game.BlockHexMutation(hex);
        internal Dictionary<int, int> UseMonopolyMutation(EnumResourceType resource) => _game.UseMonopolyMutation(resource);
        internal void UseYearOfPlentyMutation(ResourceCostOrStock resource) => _game.UseYearOfPlentyMutation(resource);
        internal void RoadBuiltMutation(Edge edge) => _game.RoadBuiltMutation(edge);
        internal void VillageBuiltMutation(Vertex vertex, bool secondVillage) => _game.VillageBuiltMutation(vertex, secondVillage);
        internal void TownPaidAndBuiltMutation(Vertex vertex) => _game.TownPaidAndBuiltMutation(vertex);
        internal void RoadPaidAndBuiltMutation(Edge edge) => _game.RoadPaidAndBuiltMutation(edge);
        internal void VillagePaidAndBuiltMutation(Vertex vertex) => _game.VillagePaidAndBuiltMutation(vertex);
        internal void BuyDevCardMutation(DevelopmentCard card) => _game.BuyDevCardMutation(card);
        internal void CardsDiscardedMutation(Player player, ResourceCostOrStock selected) => _game.CardsDiscardedMutation(player, selected);
        internal void CardsDiscardedContextMutation() => _game.CardsDiscardedContextMutation();
        internal void MarkDevCardsAsOldMutation() => _game.MarkDevCardsAsOldMutation();
        internal void AdvanceToNextPlayerMutation(int nextIndex) => _game.AdvanceToNextPlayerMutation(nextIndex);
        internal DevelopmentCard DevCardPlayedMutation(DevelopmentCard card) => _game.DevCardPlayedMutation(card);
        internal void PlayerTradeDoneMutation(Player seller, Player buyer, ResourceCostOrStock offered, ResourceCostOrStock desired) =>
    _game.PlayerTradeDoneMutation(seller, buyer, offered, desired);
        internal List<ResultDistributeResources> ServePlayersMutation() => _game.ServePlayersMutation();
        internal int DiceRolledMutation() => _game.DiceRolledMutation();
        internal void CardStolenMutation(Player victim, EnumResourceType resource) => _game.CardStolenMutation(victim, resource);

        internal void CreateCardsStealingContext(int victimId) => _game.CreateCardsStealingContext(victimId);
        internal void CreatePlayerTradeOfferedContext(int sellerId, int buyerId, string sellerName, string buyerName, ResourceCostOrStock offered, ResourceCostOrStock desired) =>
            _game.CreatePlayerTradeOfferedContext(sellerId, buyerId, sellerName, buyerName, offered, desired);
        internal void CreateCardDiscardingContext(IEnumerable<int> playersToDiscard) => _game.CreateCardDiscardingContext(playersToDiscard);
        internal void CreateTradeDraftContext(ResourceCostOrStock offered) => _game.CreateTradeDraftContext(offered);
        internal void CreateRoadBuildingContext(int roadsLeftToBuild) => _game.CreateRoadBuildingContext(roadsLeftToBuild);
        internal void RoadBuildingContextMutation() => _game.RoadBuildingContextMutation();

        internal void SetVillageBuiltThisTurn(bool built) => _game.VillagePlacedThisTurn = built;
        internal void SetRoadBuiltThisTurn(bool built) => _game.RoadPlacedThisTurn = built;
        public void SetCorePhase(EnumGamePhases newPhase) => _game.CurrentPhase = newPhase; // public for testing

        // wrappers //

        internal void WinCheck() => _game.WinCheck();
        internal void RollDice() => _game.RollDice();
    }
}