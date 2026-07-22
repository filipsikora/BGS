using Catan.Core.Models;

namespace Catan.Core.Runtime.MutationResults
{
    public class KnightChampionUpdateResult(bool changed, Player? oldChampion, Player? newChampion)
    {
        public bool Changed = changed;
        public Player? OldChampion = oldChampion;
        public Player? NewChampion = newChampion;
    }
}
