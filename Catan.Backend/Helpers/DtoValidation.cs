using Catan.Backend.Models;
using System.Text.Json;

namespace Catan.Backend.Helpers
{
    public static class DtoValidation
    {
        public static T RequireValueNotNull<T>(T? value, string fieldName) where T : struct
        {
            if (value == null)
                throw new BadRequestException($"{fieldName} is required");

            return value.Value;
        }

        public static int RequirePositiveInt(int? value, string fieldName)
        {
            if (value == null)
                throw new BadRequestException($"{fieldName} is required");

            if (value <= 0)
                throw new BadRequestException($"{fieldName} has to be over 0");

            return value.Value;
        }

        public static void EnsureNoExtraFields<T>(JsonElement json)
        {
            if (json.ValueKind != JsonValueKind.Object)
                throw new BadRequestException($"Data must be a JSON object");

            var allowedFields = typeof(T).GetProperties().Select(p => p.Name).ToHashSet(StringComparer.OrdinalIgnoreCase);

            foreach (var property in json.EnumerateObject())
                if (!allowedFields.Contains(property.Name))
                    throw new BadRequestException($"Unknown field: {property.Name}");
        }
    }
}