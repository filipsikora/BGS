using Catan.Core.Models;
using Catan.Shared.Data;

namespace Catan.Core.Runtime
{
    public sealed class GameFlowStateLogicHolder
    {
        public GameFlowStateLogicHolder() { }

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
    }
}