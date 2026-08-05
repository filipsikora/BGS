#nullable enable
using Catan.Core.Helpers;
using Catan.Core.Interfaces;
using Catan.Core.Models;
using Vertex = Catan.Core.Models.Vertex;
using Edge = Catan.Core.Models.Edge;
using Catan.Shared.Data;
using Catan.Core.Results;
using Catan.Core.Data;
using Catan.Core.Runtime.MutationResults;

namespace Catan.Core.Engine
{
    public class GameState
    {
        public List<Player> PlayerList { get; set; } = new List<Player>();

        public ResourceCostOrStock Bank { get; set; } = new ResourceCostOrStock(19, 19, 19, 10, 19);

        public List<DevelopmentCard> DevelopmentCardsDeckAvailable { get; private set; } = new();
        public List<DevelopmentCard> DevelopmentCardsDeckAll { get; private set; } = new();

        public bool AnyoneHasTenPoints { get; set; } = false;

        public Queue<int> FirstRoundsIndices { get; set; } = new();
        public EnumGamePhases CurrentPhase { get; set; } = EnumGamePhases.FirstRoundsBuilding;

        public int LastRoll { get; set; } = 0;
        public bool AfterRoll { get; set; }

        public int Turn { get; set; } = 1;

        public int? BlockedHexId { get; set; }

        public HexMap Map { get; set; }

        public List<int> FieldNumbersList { get; set; } = new List<int> { 5, 2, 6, 3, 8, 10, 9, 12, 11, 4, 8, 10, 9, 4, 5, 6, 3, 11, 12 };

        public Player? CurrentPlayer = null;

        public int CurrentPlayerIndex = 0;

        public Vertex? LastPlacedVillagePosition = null;
        public bool VillagePlacedThisTurn;
        public bool RoadPlacedThisTurn;

        public int MostKnightsUsed = 0;

        public int LongestRoad = 0;

        public Player? KnightChampion = null;

        public Player? RoadChampion = null;

        public int RequiredRoads = 5;
        public int RequiredKnights = 3;
        public int PointsReward = 2;
        public int RequiredPoints = 10;

        private readonly IRandomProvider _random;

        public PlayerTradeContext? LastPlayerTradeOffered { get; private set; }
        public CardStealingContext? CardStealingProgress { get; private set; }
        public TradeDraftContext? TradeDraft { get; private set; }
        public RoadBuildingContext? RoadBuildingProgress { get; private set; }
        public List<int> PlayersToMove = new List<int>();

        public Dictionary<EnumFieldTypes, int> FieldTypesAmount { get; set; } = new Dictionary<EnumFieldTypes, int>
            {
                { EnumFieldTypes.Wheat, 4 },
                { EnumFieldTypes.Wood, 4 },
                { EnumFieldTypes.Wool, 4 },
                { EnumFieldTypes.Stone, 3 },
                { EnumFieldTypes.Clay, 3 },
                { EnumFieldTypes.Desert, 1 }
            };

        public List<EnumFieldTypes> FieldTypesList { get; set; } = new List<EnumFieldTypes>();

        public GameState(IRandomProvider random, HexMap map)
        {
            Map = map;
            _random = random;
        }

        public int InitializeNewGame(int playerCount, float mapSize)
        {
            Map.GenerateFullMap(mapSize);

            int firstPlayerId = ReadyPlayer(playerCount);
            ReadyBoard();
            PrepareDevelopmentDeck();
            InitializeRobber();

            return firstPlayerId;
        }

        private void InitializeRobber()
        {
            var desertHex = Map.HexList.First(h => h.FieldType == EnumFieldTypes.Desert);
            BlockHexMutation(desertHex);
        }

        public void ReadyFieldList()
        {
            FieldTypesList.Clear();

            foreach (var entry in FieldTypesAmount)
            {
                int i = FieldTypesAmount[entry.Key];
                {
                    for (int value = 1; value <= i; value++)
                    {
                        FieldTypesList.Add(entry.Key);
                    }
                }
            }

            FieldTypesList.Shuffle(_random);
        }

        public void GiveHexesData(List<HexTile> hexList)
        {
            int numberIndex = 0;
            for (int index = 0; index < hexList.Count; index++)
            {
                hexList[index].FieldType = FieldTypesList[index];

                if (hexList[index].FieldType.Value != EnumFieldTypes.Desert)
                {
                    hexList[index].FieldNumber = FieldNumbersList[numberIndex];
                    numberIndex++;
                }
            }
        }

        public void RollDice()
        {
            int diceOne = _random.NextInt(1, 7);
            int diceTwo = _random.NextInt(1, 7);

            LastRoll = diceOne + diceTwo;
        }

        public int ReadyPlayer(int playerNumber)
        {
            for (int i = 1; i <= playerNumber; i++)
            {
                string name = $"Player{i}";
                Player player = new Player(name, i);

                PlayerList.Add(player);

                foreach (var key in player.Resources.ResourceDictionary.Keys.ToList())
                {
                    player.Resources.ResourceDictionary[key] = 4;
                }
            }

            FirstRoundsIndices = SetupFirstRoundsIndices(playerNumber);

            PlayerList.Shuffle(_random);
            CurrentPlayerIndex = FirstRoundsIndices.ElementAt(0);
            CurrentPlayer = PlayerList[CurrentPlayerIndex];

            return CurrentPlayer.ID;
        }

        public Queue<int> SetupFirstRoundsIndices(int playerNumber)
        {
            FirstRoundsIndices.Clear();

            for (int i = 0; i < playerNumber; i++)
                FirstRoundsIndices.Enqueue(i);

            for (int i = playerNumber - 1; i >= 0; i--)
                FirstRoundsIndices.Enqueue(i);

            return FirstRoundsIndices;
        }

        public void ReadyBoard()
        {
            ReadyFieldList();

            if (Map != null)
            {
                GiveHexesData(Map.HexList);
            }
        }

        public KnightChampionUpdateResult CheckChampionship(Player player, int playerMax)
        {
            Player? oldChampion = KnightChampion;

            if (playerMax >= RequiredKnights && playerMax > MostKnightsUsed)
            {
                MostKnightsUsed = playerMax;

                player.ExtraPoints += 2;
                KnightChampion = player;
                player.CountPoints();

                if (oldChampion != null)
                {
                    oldChampion.ExtraPoints -= 2;
                    oldChampion.CountPoints();
                }

                return new KnightChampionUpdateResult(true, oldChampion, KnightChampion);
            }

            return new KnightChampionUpdateResult(false, null, null);
        }

        private int RecomputePlayerLongestRoad(Player player)
        {
            var calculator = new QuikGraphLongestRoad(Map.VertexList, Map.Edges, player);
            int length = calculator.ComputeLongestRoad();
            player.LongestRoadCount = length;

            return length;
        }

        public RoadChampionUpdateResult UpdateRoadChampion()
        {
            Player? oldChampion = RoadChampion;

            foreach (var player in PlayerList)
            {
                RecomputePlayerLongestRoad(player);
            }

            var eligible = PlayerList.Where(p => p.LongestRoadCount >= RequiredRoads).ToList();

            if (eligible.Count == 0)
            {
                if (RoadChampion != null)
                {
                    RoadChampion.ExtraPoints -= PointsReward;
                    RoadChampion.CountPoints();
                    RoadChampion = null;
                    LongestRoad = 0;

                    return new RoadChampionUpdateResult(true, oldChampion, null);
                }

                return new RoadChampionUpdateResult(false, oldChampion, null);
            }

            int maxLength = eligible.Max(p => p.LongestRoadCount);
            var candidates = eligible.Where(p => p.LongestRoadCount == maxLength).ToList();

            if (RoadChampion != null && candidates.Contains(RoadChampion))
                return new RoadChampionUpdateResult(false, oldChampion, RoadChampion);

            var newChampion = candidates[0];

            newChampion.ExtraPoints += PointsReward;
            newChampion.CountPoints();
            RoadChampion = newChampion;
            LongestRoad = maxLength;

            if (oldChampion != null)
            {
                oldChampion.ExtraPoints -= PointsReward;
                oldChampion.CountPoints();
            }

            return new RoadChampionUpdateResult(true, oldChampion, newChampion);
        }

        public (bool village, bool road, bool town) CheckBuildOptions(IPositionData position)
        {
            if (position is Vertex)
            {
                return (true, false, true);
            }

            else
            {
                return (false, true, false);
            }
        }

        public void PrepareDevelopmentDeck()
        {
            DevelopmentCardsDeckAvailable.Clear();
            int id = 1;

            foreach (var entry in DevelopmentCardDataRegistry.TotalCount)
            {
                var type = entry.Key;
                var amount = entry.Value;

                for (int i = 0; i < amount; i++)
                {
                    DevelopmentCard card = new DevelopmentCard(type, id);
                    DevelopmentCardsDeckAvailable.Add(card);
                    DevelopmentCardsDeckAll.Add(card);
                    id++;
                }
            }

            DevelopmentCardsDeckAvailable.Shuffle(_random);
        }

        public bool WinCheck(int playerId)
        {
            return GetPlayerById(playerId).Points >= RequiredPoints;
        }

        public ResultEndGame GameWon(int playerId)
        {
            Player player = GetPlayerById(playerId);

            AnyoneHasTenPoints = true;

            return GameOver(player);
        }

        public ResultEndGame GameOver(Player winner)
        {
            Dictionary<int, int> playerScoresToIds = new();

            foreach (Player player in PlayerList)
            {
                playerScoresToIds[player.Points] = player.ID;
            }

            var result = new ResultEndGame(playerScoresToIds, winner.ID);

            return result;
        }

        public Dictionary<EnumResourceType, bool> CheckResourcesAvailabilityAfterChange(ResourceCostOrStock cardsAlreadySelected)
        {
            var availability = new Dictionary<EnumResourceType, bool>();

            foreach (var (type, amount) in Bank.ResourceDictionary)
            {
                int alreadySelected = cardsAlreadySelected.Get(type);
                availability[type] = amount - alreadySelected > 0;
            }

            return availability;
        }

        // getters //

        public int GetCardDiscardingProgress()
        {
            return CardDiscardingProgress.PlayersToDiscard.Count();
        }

        public List<int> GetCurrentPlayerDevelopmentCardIds()
        {
            return CurrentPlayer.DevelopmentCardsByID;
        }

        public Player GetCurrentPlayer()
        {
            return CurrentPlayer;
        }

        public Player GetPlayerById(int id)
        {
            return PlayerList.Find(p => p.ID == id);
        }

        public DevelopmentCard GetDevCardById(int id)
        {
            return DevelopmentCardsDeckAll.Find(c => c.ID == id);
        }

        public bool GetAfterRoll()
        {
            return AfterRoll;
        }

        public int GetRolledNumber()
        {
            return LastRoll;
        }

        public int? GetBlockedHexId()
        {
            return Map.HexList.Find(h => h.isBlocked)?.Id;
        }

        // setters //

        public void SetAfterRollTo(bool afterRoll)
        {
            AfterRoll = afterRoll;
        }

        public void SetPlayerName(string playerName, int playerId)
        {
            var player = PlayerList.Find(p => p.ID == playerId);
            player.Name = playerName;
        }

        // mutators //

        public int DiceRolledMutation()
        {
            SetAfterRollTo(true);

            return LastRoll;
        }

        public void MarkDevCardsAsOldMutation(int playerId)
        {
            var player = GetPlayerById(playerId);

            foreach (var devCardId in player.DevelopmentCardsByID)
            {
                DevelopmentCard devCard = GetDevCardById(devCardId);
                devCard.IsNew = false;
            }
        }

        public void AdvanceToNextPlayerMutation(int nextPlayerIndex)
        {
            CurrentPlayerIndex = nextPlayerIndex;
            CurrentPlayer = PlayerList[nextPlayerIndex];

            Turn++;

            SetAfterRollTo(false);
            VillagePlacedThisTurn = false;
            RoadPlacedThisTurn = false;
        }

        public KnightChampionUpdateResult DevCardPlayedMutation(DevelopmentCard card, Player player)
        {
            card.IsUsed = true;
            player.DevelopmentCardsByID.Remove(card.ID);

            switch (card.Type)
            {
                case EnumDevelopmentCardTypes.Knight:
                    return UseKnightMutation(player);

                case EnumDevelopmentCardTypes.VictoryPoint:
                    UseVictoryPointMutation(player);
                    return new KnightChampionUpdateResult(false, null, null);

                default:
                    return new KnightChampionUpdateResult(false, null, null)
            }
        }

        public void BankTradeMutation(EnumResourceType offered, EnumResourceType desired, int ratio, int playerId)
        {
            var player = GetPlayerById(playerId);

            player.Resources.SubtractExactAmount(offered, ratio);
            player.Resources.AddExactAmount(desired, 1);

            Bank.SubtractExactAmount(desired, 1);
            Bank.AddExactAmount(offered, ratio);
        }

        public void PlayerTradeDoneMutation(Player seller, Player buyer, ResourceCostOrStock offered, ResourceCostOrStock desired)
        {
            seller.Resources.AddExact(desired);
            seller.Resources.SubtractExact(offered);

            buyer.Resources.AddExact(offered);
            buyer.Resources.SubtractExact(desired);

            LastPlayerTradeOffered = null;
        }

        public void CreatePlayerTradeOfferedContext(int sellerId, int buyerId, string sellerName, string buyerName, ResourceCostOrStock offered, ResourceCostOrStock desired)
        {
            LastPlayerTradeOffered = new PlayerTradeContext(sellerId, buyerId, sellerName, buyerName, offered, desired);
            TradeDraft = null;
        }

        public void CreateTradeDraftContext(ResourceCostOrStock offered)
        {
            if (TradeDraft != null)
                return;

            TradeDraft = new TradeDraftContext(offered);
        }

        public void CreateRoadBuildingContext(int roadsLeftToBuild)
        {
            if (RoadBuildingProgress != null)
                return;

            RoadBuildingProgress = new RoadBuildingContext(roadsLeftToBuild);
        }

        public void RoadBuildingContextMutation()
        {
            RoadBuildingProgress.RoadsLeftToBuild--;

            if (RoadBuildingProgress.RoadsLeftToBuild == 0)
                RoadBuildingProgress = null;
        }

        public void CardsDiscardedMutation(Player player, ResourceCostOrStock selectedCards)
        {
            PayCostMutation(player, selectedCards);
        }

        public void CreateCardsStealingContext(int victimId)
        {
            CardStealingProgress = new CardStealingContext(victimId);
        }

        public void CardStolenMutation(Player victim, EnumResourceType resource, Player thief)
        {
            thief.Resources.AddExactAmount(resource, 1);
            victim.Resources.SubtractExactAmount(resource, 1);

            CardStealingProgress = null;
        }

        public RoadChampionUpdateResult VillageBuiltMutation(Vertex vertex, bool secondVillage = false, Player player)
        {
            var village = new BuildingVillage(player, vertex.X, vertex.Y, vertex);

            player.Buildings.Add(village);

            vertex.HasVillage = true;
            vertex.Owner = player;
            LastPlacedVillagePosition = vertex;

            if (vertex.HasPort)
            {
                player.Ports.Add(vertex.Port);
            }

            if (secondVillage)
            {
                GiveResourcesForSecondVillageMutation(vertex);
            }

            player.CountPoints();

            VillagePlacedThisTurn = true;

            return UpdateRoadChampion();
        }

        public RoadChampionUpdateResult RoadBuiltMutation(Edge edge, Player player)
        {
            var road = new BuildingRoad(player, edge.X, edge.Y, edge);
            player.Buildings.Add(road);

            edge.Owner = player;

            RoadPlacedThisTurn = true;

            return UpdateRoadChampion();
        }

        public void TownBuiltMutation(Vertex vertex, Player player)
        {
            var town = new BuildingTown(player, vertex.X, vertex.Y, vertex);
            var village = player.Buildings.FirstOrDefault(b => b is BuildingVillage v && v.Vertex == vertex);

            player.Buildings.Remove(village);
            player.Buildings.Add(town);

            vertex.HasVillage = false;
            vertex.HasTown = true;

            player.CountPoints();
        }

        public RoadChampionUpdateResult VillagePaidAndBuiltMutation(Vertex vertex, int playerId)
        {
            var player = GetPlayerById(playerId);
            PayCostMutation(player, BuildingVillage.Cost);
            return VillageBuiltMutation(vertex, false, player);
        }

        public RoadChampionUpdateResult RoadPaidAndBuiltMutation(Edge edge, int playerId)
        {
            var player = GetPlayerById(playerId);
            PayCostMutation(player, BuildingRoad.Cost);
            return RoadBuiltMutation(edge, player);
        }

        public void TownPaidAndBuiltMutation(Vertex vertex, int playerId)
        {
            var player = GetPlayerById(playerId);
            PayCostMutation(player, BuildingTown.Cost);
            TownBuiltMutation(vertex, player);
        }

        public void GiveResourcesForSecondVillageMutation(Vertex vertex)
        {
            foreach (HexTile hex in vertex.AdjacentHexTiles)
            {
                var resourceType = hex.GetResourceType();

                if (resourceType.HasValue)
                {
                    vertex.Owner?.Resources.AddExactAmount(resourceType.Value, 1);
                    Bank.SubtractExactAmount(resourceType.Value, 1);
                }
            }
        }

        public void PayCostMutation(Player player, ResourceCostOrStock cost)
        {
            foreach (var resource in cost.ResourceDictionary.Keys)
            {
                player.Resources.ResourceDictionary[resource] -= cost.ResourceDictionary[resource];
                Bank.ResourceDictionary[resource] += cost.ResourceDictionary[resource];
            }
        }

        public void BuyDevCardMutation(DevelopmentCard devCard)
        {
            var player = GetCurrentPlayer();

            PayCostMutation(player, DevelopmentCard.Cost);

            DevelopmentCardsDeckAvailable.Remove(devCard);
            player.DevelopmentCardsByID.Add(devCard.ID);

            devCard.Owner = player;
            devCard.IsNew = true;
        }

        public Dictionary<int, int> UseMonopolyMutation(EnumResourceType resource, Player currentPlayer)
        {
            var victimsIdsAndAmounts = new Dictionary<int, int>();
            var playerListCopy = PlayerList.ToList();

            playerListCopy.Remove(currentPlayer);

            foreach (var player in playerListCopy)
            {
                var amount = player.Resources.ResourceDictionary[resource];

                currentPlayer.Resources.AddExactAmount(resource, amount);
                player.Resources.SubtractExactAmount(resource, amount);
                victimsIdsAndAmounts.Add(player.ID, amount);
            }

            return victimsIdsAndAmounts;
        }

        public void UseYearOfPlentyMutation(ResourceCostOrStock requested, int playerId)
        {
            var player = GetPlayerById(playerId);

            foreach (var (type, amount) in requested.ResourceDictionary)
            {
                player.Resources.AddExactAmount(type, amount);
                Bank.SubtractExactAmount(type, amount);
            }
        }

        public KnightChampionUpdateResult UseKnightMutation(Player player)
        {
            player.KnightsUsed++;

            return CheckChampionship(player, player.KnightsUsed);
        }

        public void UseVictoryPointMutation(Player player)
        {
            player.VictoryPointsCardsUsed++;
            player.CountPoints();
        }

        public List<ResultDistributeResources> ServePlayersMutation()
        {
            var resultList = new List<ResultDistributeResources>();

            if (Map == null)
                return resultList;

            foreach (HexTile hex in Map.HexList)
            {
                if (hex.FieldType == EnumFieldTypes.Desert)
                    continue;

                if (hex.isBlocked)
                    continue;

                if (hex.FieldNumber == LastRoll)
                {
                    foreach (Vertex vertex in hex.AdjacentVertices)
                    {
                        if (!vertex.IsOwned)
                            continue;

                        else
                        {
                            int requested = vertex.HasVillage ? 1 : 2;
                            Player owner = vertex.Owner;
                            EnumResourceType type = hex.GetResourceType().GetValueOrDefault();

                            int granted = Bank.SubtractUpTo(type, requested);
                            owner.Resources.AddExactAmount(type, granted);

                            var result = new ResultDistributeResources(owner.ID, owner.Name, type, requested, granted);
                            resultList.Add(result);
                        }
                    }
                }
            }

            return resultList;
        }

        public void BlockHexMutation(HexTile hex)
        {
            foreach (var hexTile in Map.HexList)
            {
                if (hexTile.isBlocked)
                {
                    hexTile.isBlocked = false;
                }
            }

            hex.isBlocked = true;
            BlockedHexId = hex.Id;
        }

        // end //
    }
}