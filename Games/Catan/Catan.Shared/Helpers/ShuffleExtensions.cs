using Catan.Shared.Interfaces;
using System.Collections.Generic;

namespace Catan.Shared.Helpers
{
    public static class ShuffleExtensions
    {
        public static void Shuffle<T>(this IList<T> list, IRandomProvider random)
        {
            for (int i = list.Count - 1; i > 0; i--)
            {
                int j = random.NextInt(0, i + 1);

                (list[i], list[j]) = (list[j], list[i]);
            }
        }
    }
}