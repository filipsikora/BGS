using Catan.Core.Interfaces;

namespace Catan.Core.Helpers
{
    public class RandomProvider : IRandomProvider
    {
        private readonly Random _random = new();

        public int NextInt(int min, int max)
        {
            return _random.Next(min, max);
        }
    }
}