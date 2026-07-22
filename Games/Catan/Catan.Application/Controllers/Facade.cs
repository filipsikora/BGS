using Catan.Core.Models;
using Catan.Core.Queries.Interfaces;
using Catan.Core.Results;
using Catan.Core.Runtime;
using Catan.Core.Snapshots.ClientQueries;
using Catan.Core.Snapshots.Persistence;
using Catan.Shared.Data;

namespace Catan.Application.Controllers
{
    public sealed class Facade
    {
        private readonly GameSession _session;

        private readonly IBoardQueryService _boardQuery;
        private readonly IDevCardsQueryService _devCardQuery;
        private readonly IPlayersQueryService _playersQuery;
        private readonly IResourcesQueryService _resourcesQuery;
        private readonly ITradeQueryService _tradeQuery;
        private readonly ITurnsQueryService _turnsQuery;

        public Facade(GameSession session, IBoardQueryService boardQuery, IDevCardsQueryService devcardQuery, IPlayersQueryService playersQuery, 
            IResourcesQueryService resourcesQuery, ITradeQueryService tradeQuery, ITurnsQueryService turnsQuery)
        {
            _session = session;
            _boardQuery = boardQuery;
            _devCardQuery = devcardQuery;
            _playersQuery = playersQuery;
            _resourcesQuery = resourcesQuery;
            _tradeQuery = tradeQuery;
            _turnsQuery = turnsQuery;
        }

        // setters //

        public void SetCorePhase(EnumGamePhases phase) => _session.SetCorePhase(phase);
        public void SetPlayerName(string playerName, int playerId) => _session.SetPlayerName(playerName, playerId);
        public void SetPlayersToMove(List<int> playersToMove) => _session.SetPlayersToMove(playersToMove);
        public void RemovePlayerFromToMove(int playerId) => _session.RemovePlayerFromToMove(playerId);

        // getters//
        public EnumGamePhases GetNextPhaseFromAfterRoll() => _session.GetNextPhaseFromAfterRoll();
        public EnumGamePhases? GetNextPhaseAfterDiscarding() => _session.GetNextPhaseAfterDiscarding();

        public int GetPlayerTradeRatioById(EnumResourceType resource, int playerId) => _session.GetPlayerTradeRatioById(resource, playerId);
        public bool PlayerHasEnoughResources(int playerAmount, int neededAmount) => _session.PlayerHasEnoughResources(playerAmount, neededAmount);
        public int GetPlayerResourceAmountById(EnumResourceType resource, int playerId) => _session.GetPlayerResourceAmountById(resource, playerId);
        public int GetCurrentPlayerId() => _session.GetCurrentPlayerId();

        public List<int> GetAdjacentToHexPlayersIds(int hexId) => _session.GetAdjacentToHexPlayersIds(hexId);

        public List<int> GetPossibleVictimsIds() => _session.GetPossibleVictimsIds();

        public bool CanPlayerDiscard(ResourceCostOrStock resourcesSelected, int discardingPlayerId) => _session.CanPlayerDiscard(resourcesSelected, discardingPlayerId);

        public int GetVictimId() => _session.GetVictimId();

        public bool GetAfterRoll() => _session.GetAfterRoll();

        public int GetLastPlacedVillagePositionId() => _session.GetLastPlacedVillagePositionId();

        public bool GetRoadsLeftToBuild() => _session.GetRoadsLeftToBuild();

        public bool CheckIfCardsSelected(ResourceCostOrStock resources) => _session.CheckIfCardsSelected(resources);

        public bool CheckIfExactCardsAmountSelected(ResourceCostOrStock resources, int amount) => _session.CheckIfExactCardsAmountSelected(resources, amount);

        public int GetDesertHexId() => _session.GetDesertHexId();

        public (bool village, bool road, bool town) GetVertexBuildOptions(int vertexId, int playerId) => _session.GetVertexBuildOptions(vertexId, playerId);
        public (bool village, bool road, bool town) GetEdgeBuildOptions(int edgeId) => _session.GetEdgeBuildOptions(edgeId);

        public IEnumerable<int> GetIdsList() => _session.GetIdsList();

        public List<int> GetPlayersToMove() => _session.GetPlayersToMove();
        public List<int> GetPlayersToDiscard() => _session.GetPlayersToDiscard();
        public Dictionary<EnumResourceType,int> GetPlayerCardsById(int playerId) => _session.GetPlayerCardsById(playerId).ToDictionary();
        public Dictionary<EnumResourceType, int> GetBank() => _session.GetBank().ToDictionary();
        public int GetBuyerId() => _session.GetBuyerId();




        // use cases//

        public ResultBankTrade UseBankTrade(EnumResourceType offered, EnumResourceType desired, int playerId) => _session.UseBankTrade(offered, desired, playerId);
        public ResultBlockHex UseBlockHex(int hexId) => _session.UseBlockHex(hexId);
        public ResultCondition UseSelectVictim(int victimId) => _session.UseSelectVictim(victimId);
        public ResultRollDice UseRollDice() => _session.UseRollDice();
        public ResultCondition UseDiscard(int discardingPlayerId, ResourceCostOrStock resourcesSelected) => _session.UseDiscard(discardingPlayerId, resourcesSelected);
        public ResultStealResource UseSteal(int victimId, EnumResourceType resource, int thiefId) => _session.UseSteal(victimId, resource, thiefId);
        public ResultPlayDevCard UseDevCard(int cardId, int playerId) => _session.UseDevCard(cardId, playerId);
        public ResultBuildInitialRoad UseBuildInitialRoad(int edgeId, int vertexId, int playerId) => _session.UseBuildInitialRoad(edgeId, vertexId, playerId);
        public ResultBuildInitialVillage UseBuildInitialVillage(int vertexId, int playerId) => _session.UseBuildInitialVillage(vertexId, playerId);
        public ResultBuildRoad UseBuildRoad(int edgeId, int playerId) => _session.UseBuildRoad(edgeId, playerId);
        public ResultBuildVillage UseBuildVillage(int vertexId, int playerId) => _session.UseBuildVillage(vertexId, playerId);
        public ResultBuildFreeRoad UseBuildFreeRoad(int edgeId, int playerId) => _session.UseBuildFreeRoad(edgeId, playerId);
        public ResultFinishTurn UseFinishTurn(int playerId) => _session.UseFinishTurn(playerId);
        public ResultMonopolyCard UseMonopolyCard(EnumResourceType resource, int playerId) => _session.UseMonopolyCard(resource, playerId);
        public ResultBuyDevCard UseBuyDevCard(int playerId) => _session.UseBuyDevCard(playerId);
        public ResultUpgradeVillage UseUpgradeVillage(int vertexId, int playerId) => _session.UseUpgradeVillage(vertexId, playerId);
        public ResultCondition UsePrepareTrade(ResourceCostOrStock offered, int playerId) => _session.UsePrepareTrade(offered, playerId);
        public ResultPlayerTrade UseOfferTrade(int buyerId, ResourceCostOrStock desired, int sellerId) => _session.UseOfferTrade(buyerId, desired, sellerId);
        public ResultPlayerTrade UseReactToTrade() => _session.UseReactToTrade();
        public ResultYearOfPlenty UseYearOfPlenty(ResourceCostOrStock resources) => _session.UseYearOfPlenty(resources);

        // queries //

        public BoardSnapshot GetBoardData() => _boardQuery.GetBoardData();
        public PlayerDataSnapshot GetPlayersData(int playerId) => _playersQuery.GetPlayersData(playerId);
        public ResourcesAvailabilitySnapshot GetResourcesAvailability() => _resourcesQuery.GetResourcesAvailability();
        public PlayerResourcesSnapshot GetPlayersCards(int playerId) => _playersQuery.GetPlayersCards(playerId);
        public PlayerResourcesSnapshot GetVictimsCards() => _playersQuery.GetVictimsCards();
        public IReadOnlyList<PlayerNameSnapshot> GetSomePlayersNames(List<int> potentialVictimsIds) => _playersQuery.GetSomePlayersNames(potentialVictimsIds);
        public IReadOnlyList<DevelopmentCardSnapshot> GetCurrentPlayerDevCards() => _devCardQuery.GetCurrentPlayerDevCards();

        public IReadOnlyList<PlayerNameSnapshot> GetNotCurrentPlayersNames() => _playersQuery.GetNotCurrentPlayersNames();
        public TradeOfferedSnapshot GetTradeOfferData() => _tradeQuery.GetTradeOfferData();
        public FullPlayerSnapshot GetFullPlayerData(int playerId) => _playersQuery.GetFullPlayerData(playerId);

        // gamestatesnapshot //

        public GameStateSnapshot GetGameStateData() => _session.GetGameStateData();
        public GameStatePerPlayerSnapshot GetGameStatePerPlayerData(int playerId) => _session.GetGameStatePerPlayerData(playerId);
    }
}