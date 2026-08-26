using Catan.Core.Conditions;
using Catan.Core.Engine;
using Catan.Core.Models;
using Catan.Core.UseCases;
using Catan.Core.Results;
using Catan.Core.Rules;
using Catan.Shared.Data;
using Catan.Core.Snapshots.ClientQueries;
using Catan.Core.Queries.GameStateSnapshotBuilders;
using Catan.Core.Snapshots.Persistence;
using Catan.Core.Runtime.MutationResults;

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

        public ResultBankTrade UseBankTrade(EnumResourceType offered, EnumResourceType desired, int playerId) => _bankTrade.Handle(offered, desired, playerId);
        public ResultBlockHex UseBlockHex(int hexId) => _blockHex.Handle(hexId);
        public ResultCondition UseSelectVictim(int victimId) => _selectVictim.Handle(victimId);
        public ResultBuildFreeRoad UseBuildFreeRoad(int edgeId, int playerId) => _buildFreeRoad.Handle(edgeId, playerId);
        public ResultBuildInitialRoad UseBuildInitialRoad(int edgeId, int vertexId, int playerId) => _buildInitialRoad.Handle(edgeId, vertexId, playerId);
        public ResultBuildInitialVillage UseBuildInitialVillage(int vertexId, int playerId) => _buildInitialVillage.Handle(vertexId, playerId);
        public ResultBuildRoad UseBuildRoad(int edgeId, int playerId) => _buildRoad.Handle(edgeId, playerId);
        public ResultBuildVillage UseBuildVillage(int vertexId, int playerId) => _buildVillage.Handle(vertexId, playerId);
        public ResultUpgradeVillage UseUpgradeVillage(int vertexId, int playerId) => _upgradeVillage.Handle(vertexId, playerId);
        public ResultRollDice UseRollDice() => _rollDice.Handle();
        public ResultCondition UseDiscard(int discardingPlayerId, ResourceCostOrStock resourcesSelected) => _discardCards.Handle(discardingPlayerId, resourcesSelected);
        public ResultStealResource UseSteal(int victimId, EnumResourceType resource, int thiefId) => _stealCard.Handle(victimId, resource, thiefId);
        public ResultPlayDevCard UseDevCard(int cardId, int playerId) => _playDevCard.Handle(cardId, playerId);
        public ResultFinishTurn UseFinishTurn(int playerId) => _finishTurn.Handle(playerId);
        public ResultMonopolyCard UseMonopolyCard(EnumResourceType resource, int playerId) => _useMonopoly.Handle(resource, playerId);
        public ResultBuyDevCard UseBuyDevCard(int playerId) => _buyDevCard.Handle(playerId);
        public ResultCondition UsePrepareTrade(ResourceCostOrStock offered, int playerId) => _prepareTrade.Handle(offered, playerId);
        public ResultPlayerTrade UseOfferTrade(int buyerId, ResourceCostOrStock desired, int sellerId) => _offerTrade.Handle(buyerId, desired, sellerId);
        public ResultPlayerTrade UseReactToTrade() => _reactToTrade.Handle();
        public ResultYearOfPlenty UseYearOfPlenty(ResourceCostOrStock resources, int playerId) => _useYearOfPlenty.Handle(resources, playerId);


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
        public int? GetBlockedHexId() => _game.GetBlockedHexId();
        public List<(int HexQ, int HexR, int CornerIndex)> GetVertexCorners(int vertexId) => _boardReader.GetVertexCorners(GetVertexById(vertexId));
        public VertexSnapshot GetVertexData(int vertexId) => _boardReader.GetVertexData(GetVertexById(vertexId));
        public EdgeSnapshot GetEdgeData(int edgeId) => _boardReader.GetEdgeData(GetEdgeById(edgeId));
        public HexSnapshot GetHexData(int hexId) => _boardReader.GetHexData(GetHexById(hexId));
        public PortSnapshot GetPortData(int edgeId) => _boardReader.GetPortData(GetEdgeById(edgeId), GetPortByEdge(GetEdgeById(edgeId)));
        public List<int> GetAdjacentToHexPlayersIds(int hexId) => _boardReader.GetAdjacentToHexPlayersIds(GetHexById(hexId));

        // players //

        internal Player GetCurrentPlayer() => _game.CurrentPlayer ?? throw new InvalidOperationException("CurrentPlayer not initialized");
        internal Player GetPlayerById(int playerId) => _game.GetPlayerById(playerId);
        internal Player GetPlayerByIndex(int index) => _game.PlayerList[index];
        internal List<Player> GetPlayersByIds(List<int> playersIds) => playersIds.Select(GetPlayerById).ToList();
        internal IEnumerable<Player> GetAllPlayersView()
        {
            foreach (var player in _game.PlayerList)
                yield return player;
        }

        public int GetCurrentPlayerId() => GetCurrentPlayer().ID;
        public int GetPlayersRoadsLeftById(int playerId) => _game.GetPlayerById(playerId).BuildingCount(EnumBuildings.Road);
        public int GetPlayerResourceAmountById(EnumResourceType resource, int playerId) => _game.GetPlayerById(playerId).Resources.Get(resource);
        public bool PlayerHasEnoughResources(int playerAmount, int neededAmount) => ConditionsTrade.PlayerHasEnoughResource(playerAmount, neededAmount).Success;
        public List<PlayerNameSnapshot> GetAllPlayersNamesData() => _playersReader.GetAllPlayersNames(_game.PlayerList);
        public List<PlayerNameSnapshot> GetSomePlayersNamesData(List<int> playersIds) => _playersReader.GetSomePlayersNames(playersIds.Select(GetPlayerById).ToList());
        public List<PlayerNameSnapshot> GetNotCurrentPlayerNamesData() => _playersReader.GetNotCurrentPlayersNames(_game.PlayerList.Where(p => p.ID != GetCurrentPlayerId()));
        public PlayerResourcesSnapshot GetPlayerssResourcesData(int playerId) => _playersReader.GetPlayersCards(GetPlayerById(playerId));
        public PlayerDataSnapshot GetPlayerData(int playerId) => _playersReader.GetPlayersData(GetPlayerById(playerId));
        public PlayerResourcesSnapshot GetVictimCardsData() => _playersReader.GetVictimsCards(GetPlayerById(GetVictimId()));
        public FullPlayerSnapshot GetFullPlayerData(int playerId) => _playersReader.GetFullPlayerData(GetPlayerById(playerId), GetPlayerDevCardsByIdData(playerId));
        public OtherPlayersSnapshot GetOtherPlayersData(int playerId) => _playersReader.GetOtherPlayersData(_game.PlayerList.Where(p => p != GetPlayerById(playerId)));


        // game flow //

        public bool GetAfterRoll() => _game.GetAfterRoll();
        public int GetTurn() => _game.Turn;
        public int GetLastRoll() => _game.LastRoll;
        public int? GetKnightChampionId() => _game.KnightChampion != null ? _game.KnightChampion.ID : null;
        public int? GetRoadChampionId() => _game.RoadChampion != null ? _game.RoadChampion.ID : null;
        public IEnumerable<int> GetIdsList() => _game.PlayerList.Select(p => p.ID);
        public List<int> GetPlayersToMove() => _game.PlayersToMove;


        // thief //

        internal (bool exists, CardStealingContext context) TryGetCardStealingContext()
        {
            var context = _game.CardStealingProgress;

            return (context != null, context);
        }
        internal CardStealingContext? GetCardStealingContext() => _game.CardStealingProgress;

        public bool GetPlayersLeftToDiscard() => _thiefReader.GetPlayersLeftToDiscard(_game.PlayerList);
        public bool CanPlayerDiscard(ResourceCostOrStock resourcesSelected, int discardingPlayerId) => _thiefReader.CanPlayerDiscard(resourcesSelected, GetPlayerById(discardingPlayerId));
        public List<int> GetPlayersToDiscard() => _thiefReader.GetCardsDiscardingPlayers(_game.PlayerList);

        public int GetVictimId() => _game.CardStealingProgress.VictimId;
        public List<int> GetPossibleVictimsIds() => _thiefReader.GetPossibleVictimsIds(GetPlayersByIds(GetAdjacentToHexPlayersIds(_game.BlockedHexId.Value)), _game.CurrentPlayer);
        public int GetBuyerId() => _game.LastPlayerTradeOffered.BuyerId;

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
        public bool CheckIfInitialRoundsRemaining() => _game.FirstRoundsIndices.Count > 0;

        // trade //

        internal ResourceCostOrStock GetOfferedResources() => _game.TradeDraft.Offered;
        internal PlayerTradeContext? TryGetPlayerTradeContext() => _game.LastPlayerTradeOffered;
        internal TradeDraftContext? TryGetTradeDraftContext() => _game.TradeDraft;

        public int GetPlayerTradeRatioById(EnumResourceType resource, int playerId) => _tradeReader.GetPlayerTradeRatioById(resource, _game.GetPlayerById(playerId), 
            _game.Map.PortList.Find(port => port.Type == resource));

        // resources //

        public ResourceCostOrStock GetBank() => _game.Bank;

        public ResourcesAvailabilitySnapshot GetResourcesAvailabilityData() => _gameFlowReader.GetResourcesAvailabilityData(GetBank());
        public bool CheckIfCardsSelected(ResourceCostOrStock resources) => resources.Total() > 0;
        public bool CheckIfExactCardsAmountSelected(ResourceCostOrStock resources, int amount) => ConditionsResources.HasExactResourcesNumber(resources, amount).Success;
        public (int, bool) GetNextIndex() => _gameFlowReader.GetNextIndex(_game.FirstRoundsIndices, _game.CurrentPlayerIndex, _game.PlayerList.Count);
        public ResourceCostOrStock GetPlayerCardsById(int playerId) => _game.GetPlayerById(playerId).Resources;

        // dev cards //

        internal DevelopmentCard GetFirstDevCard() => _game.DevelopmentCardsDeckAvailable[0];
        internal DevelopmentCard GetDevCardById(int cardId) => _game.GetDevCardById(cardId);
        internal List<DevelopmentCard> GetDevCardsLeft() => _game.DevelopmentCardsDeckAvailable;

        public List<int> GetPlayersKnightCardsIds(int playerId) => _devCardsReader.GetPlayersKnightCardsIds(GetPlayerById(playerId).DevelopmentCardsByID.Select(id => GetDevCardById(id)).ToList());
        public List<DevelopmentCardSnapshot> GetDevCardsInBankData() => _gameFlowReader.GetDevCardsInBankData(_game.DevelopmentCardsDeckAvailable);
        public IReadOnlyList<DevelopmentCardSnapshot> GetCurrentPlayerDevCardsData() => _devCardsReader.GetCurrentPlayerDevCardsData(_game.CurrentPlayer.DevelopmentCardsByID.Select(id => GetDevCardById(id)).ToList(), GetAfterRoll());
        public IReadOnlyList<DevelopmentCardSnapshot> GetPlayerDevCardsByIdData(int playerId) => _devCardsReader.GetPlayerDevCardsByIdData(GetPlayerById(playerId).DevelopmentCardsByID.Select(id => GetDevCardById(id)).ToList(), GetAfterRoll());

        // GameStateSnapshot //

        public GameStateSnapshot GetGameStateData() => GameStateSnapshotBuilder.GetGameStateData();
        public GameStatePerPlayerSnapshot GetGameStatePerPlayerData(int playerId) => GameStateSnapshotBuilder.GetGameStatePerPlayerData(playerId);

        // internal setters //

        internal void BankTradeMutation(EnumResourceType offered, EnumResourceType desired, int ratio, int playerId) => _game.BankTradeMutation(offered, desired, ratio, playerId);
        internal void BlockHexMutation(HexTile hex) => _game.BlockHexMutation(hex);
        internal Dictionary<int, int> UseMonopolyMutation(EnumResourceType resource, Player player) => _game.UseMonopolyMutation(resource, player);
        internal void UseYearOfPlentyMutation(ResourceCostOrStock resource, int playerId) => _game.UseYearOfPlentyMutation(resource, playerId);
        internal RoadChampionUpdateResult RoadBuiltMutation(Edge edge, Player player) => _game.RoadBuiltMutation(edge, player);
        internal void VillageBuiltMutation(Vertex vertex, bool secondVillage, Player player) => _game.VillageBuiltMutation(vertex, player, secondVillage);
        internal void TownPaidAndBuiltMutation(Vertex vertex, int playerId) => _game.TownPaidAndBuiltMutation(vertex, playerId);
        internal RoadChampionUpdateResult RoadPaidAndBuiltMutation(Edge edge, int playerId) => _game.RoadPaidAndBuiltMutation(edge, playerId);
        internal RoadChampionUpdateResult VillagePaidAndBuiltMutation(Vertex vertex, int playerId) => _game.VillagePaidAndBuiltMutation(vertex, playerId);
        internal void BuyDevCardMutation(DevelopmentCard card) => _game.BuyDevCardMutation(card);
        internal void CardsDiscardedMutation(Player player, ResourceCostOrStock selected) => _game.CardsDiscardedMutation(player, selected);
        internal void MarkDevCardsAsOldMutation(int playerId) => _game.MarkDevCardsAsOldMutation(playerId);
        internal void AdvanceToNextPlayerMutation(int nextIndex) => _game.AdvanceToNextPlayerMutation(nextIndex);
        internal KnightChampionUpdateResult DevCardPlayedMutation(DevelopmentCard card, Player player) => _game.DevCardPlayedMutation(card, player);
        internal void PlayerTradeDoneMutation(Player seller, Player buyer, ResourceCostOrStock offered, ResourceCostOrStock desired) =>
    _game.PlayerTradeDoneMutation(seller, buyer, offered, desired);
        internal List<ResultDistributeResources> ServePlayersMutation() => _game.ServePlayersMutation();
        internal int DiceRolledMutation() => _game.DiceRolledMutation();
        internal void CardStolenMutation(Player victim, EnumResourceType resource, Player thief) => _game.CardStolenMutation(victim, resource, thief);

        internal void CreateCardsStealingContext(int victimId) => _game.CreateCardsStealingContext(victimId);
        internal void CreatePlayerTradeOfferedContext(int sellerId, int buyerId, string sellerName, string buyerName, ResourceCostOrStock offered, ResourceCostOrStock desired) =>
            _game.CreatePlayerTradeOfferedContext(sellerId, buyerId, sellerName, buyerName, offered, desired);
        internal void CreateTradeDraftContext(ResourceCostOrStock offered) => _game.CreateTradeDraftContext(offered);
        internal void CreateRoadBuildingContext(int roadsLeftToBuild) => _game.CreateRoadBuildingContext(roadsLeftToBuild);
        internal void RoadBuildingContextMutation() => _game.RoadBuildingContextMutation();

        internal void SetVillageBuiltThisTurn(bool built) => _game.VillagePlacedThisTurn = built;
        internal void SetRoadBuiltThisTurn(bool built) => _game.RoadPlacedThisTurn = built;
        public void SetCorePhase(EnumGamePhases newPhase) => _game.CurrentPhase = newPhase; // public for testing

        public void SetPlayerName(string playerName, int playerId) => _game.SetPlayerName(playerName, playerId);
        public void SetPlayersToMove(List<int> playersToMove) => _game.PlayersToMove = playersToMove;
        public void RemovePlayerFromToMove(int playerId) => _game.PlayersToMove.Remove(playerId);

        // wrappers //

        internal bool WinCheck(int playerId) => _game.WinCheck(playerId);
        internal ResultEndGame GameWon(int playerId) => _game.GameWon(playerId);
        internal void RollDice() => _game.RollDice();
    }
}