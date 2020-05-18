namespace Meeting.GatewayAPI
{
    using Refit;
    using System.Text.Json;

    public static class Defaults
    {
        public static JsonSerializerOptions JsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = false,
        };

        public static RefitSettings RefitSettings = new RefitSettings
        {
            ContentSerializer = new JsonContentSerializer()
        };
    }
}
