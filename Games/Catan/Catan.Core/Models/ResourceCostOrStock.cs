#nullable enable
using Catan.Shared.Data;

namespace Catan.Core.Models
{
    public class ResourceCostOrStock
    {
        public ResourceCostOrStock(int Wheat = 0, int Wood = 0, int Wool = 0, int Stone = 0, int Clay = 0)
        {
            ResourceDictionary = new Dictionary<EnumResourceType, int>()
            {
                { EnumResourceType.Wheat, Wheat },
                { EnumResourceType.Wood, Wood },
                { EnumResourceType.Wool, Wool },
                { EnumResourceType.Stone, Stone },
                { EnumResourceType.Clay, Clay }
            };
        }

        public Dictionary<EnumResourceType, int> ResourceDictionary { get; }

        public string? Name { get; set; } = null;

        public int Get(EnumResourceType type)
        {
            return ResourceDictionary.TryGetValue(type, out var v) ? v : 0;
        }

        public void Set(EnumResourceType resource, int amount)
        {
            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount));
            }

            ResourceDictionary[resource] = amount;
        }

        public int SubtractUpTo(EnumResourceType type, int amountWanted)
        {
            if (amountWanted < 0)
                return (0);

            int available = Get(type);
            int actualAmount = Math.Min(amountWanted, available);

            ResourceDictionary[type] -= actualAmount;

            return actualAmount;
        }

        public void AddExactAmount(EnumResourceType type, int amount)
        {
            if (amount < 0)
                return;

            ResourceDictionary[type] += amount;
        }

        public void SubtractExactAmount(EnumResourceType type, int amount)
        {
            if (amount < 0)
                return;

            ResourceDictionary[type] -= amount;
        }

        public void AddExact(ResourceCostOrStock other)
        {
            foreach (var (type, amount) in other.ResourceDictionary)
            {
                if (amount < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(amount));
                }

                ResourceDictionary[type] += amount;
            }
        }

        public void SubtractExact(ResourceCostOrStock other)
        {
            foreach (var (type, amount) in other.ResourceDictionary)
            {
                if (amount < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(amount));
                }

                ResourceDictionary[type] -= amount;
            }
        }

        public bool HasEnoughCards(ResourceCostOrStock other)
        {
            foreach (var entry in other.ResourceDictionary)
            {
                EnumResourceType type = entry.Key;
                int required = entry.Value;

                if (required <= 0)
                    continue;

                int available = ResourceDictionary.ContainsKey(type) ? ResourceDictionary[type] : 0;

                if (available < required)
                    return false;
            }

            return true;
        }

        public ResourceCostOrStock Clone()
        {
            var copy = new ResourceCostOrStock();

            foreach (var kv in ResourceDictionary)
            {
                copy.ResourceDictionary[kv.Key] = kv.Value;
            }

            return copy;
        }

        public void Clear()
        {
            foreach (var (key, value) in ResourceDictionary)
            {
                ResourceDictionary[key] = 0;
            }
        }

        public int Total()
        {
            return ResourceDictionary.Values.Sum();
        }

        public Dictionary<EnumResourceType, int> ToDictionary()
        {
            var toDictionary = new Dictionary<EnumResourceType, int>();

            foreach (var (key, value) in ResourceDictionary)
            {
                toDictionary.Add(key, value);
            }

            return toDictionary;
        }
    }
}