using Catan.Backend.Models;
using Newtonsoft.Json.Linq;

namespace Catan.Backend.Helpers
{
    public static class DtoValidation
    {
        public static void RequireValueNotNull<T>(T? value, string fieldName) where T : struct
        {
            if (value == null)
                throw new BadRequestException($"{fieldName} is required");
        }

        public static void RequirePositiveInt(int? value, string fieldName)
        {
            if (value == null)
                throw new BadRequestException($"{fieldName} is required");

            if (value <= 0)
                throw new BadRequestException($"{fieldName} has to be over 0");
        }

        public static void EnsureNoExtraFields<T>(JObject json)
        {
            if (json.Type != JTokenType.Object)
                throw new BadRequestException($"Data must be a JSON object");

            var allowedFields = typeof(T).GetProperties().Select(p => p.Name).ToHashSet(StringComparer.OrdinalIgnoreCase);

            foreach (var property in json.Properties())
                if (!allowedFields.Contains(property.Name))
                    throw new BadRequestException($"Unknown field: {property.Name}");
        }
    }
}