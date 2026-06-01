using Catan.Core.Models;
using Catan.Core.Snapshots.ClientQueries;
using Catan.Shared.Data;

namespace Catan.Core.Runtime
{
    public sealed class GameFlowStateReader
    {
        public GameFlowStateReader() { }

        public (int, bool) GetNextIndex(Queue<int> firstRoundsIndices, int currentPlayerIndex, int playerNumber)
        {
            bool initialRoundsRemaining;
            int nextIndex;

            if (firstRoundsIndices.Count > 0)
            {
                firstRoundsIndices.Dequeue();

                initialRoundsRemaining = firstRoundsIndices.Count > 0;
                nextIndex = initialRoundsRemaining ? firstRoundsIndices.Peek() : 0;

                return (nextIndex, initialRoundsRemaining);
            }

            initialRoundsRemaining = false;
            nextIndex = (currentPlayerIndex + 1) % playerNumber;

            return (nextIndex, initialRoundsRemaining);
        }

        public ResourcesAvailabilitySnapshot GetResourcesAvailabilityData(ResourceCostOrStock bank)
        {
            var resourcesAvailability = new Dictionary<EnumResourceType, bool>();

            foreach (var (type, amount) in bank.ResourceDictionary)
            {
                bool available = amount > 0;

                resourcesAvailability.Add(type, available);
            }

            return new ResourcesAvailabilitySnapshot(resourcesAvailability);
        }

        public List<DevelopmentCardSnapshot> GetDevCardsInBankData(List<DevelopmentCard> devCardsLeft)
        {
            return devCardsLeft.Select(devCard => new DevelopmentCardSnapshot(
                devCard.ID,
                devCard.Type,
                devCard.IsNew,
                false)).ToList();
        }
    }

}