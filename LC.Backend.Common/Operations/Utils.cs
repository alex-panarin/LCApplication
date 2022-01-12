using System.Text.Json;
using System.Text.Json.Serialization;

namespace LC.Backend.Common.Operations
{
    public static class Utils
    {
        private static readonly JsonSerializerOptions _options = new JsonSerializerOptions
        {
            IgnoreReadOnlyFields = true,
            IgnoreReadOnlyProperties = true,
            PropertyNameCaseInsensitive = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        public static JsonSerializerOptions JsonOptions => _options;
    }
}
