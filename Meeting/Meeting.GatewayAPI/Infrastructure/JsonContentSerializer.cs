namespace Meeting.GatewayAPI
{
    using Refit;
    using System.Net.Http;
    using System.Text;
    using System.Text.Json;
    using System.Threading.Tasks;

    public class JsonContentSerializer : IContentSerializer
    {
        public async Task<T> DeserializeAsync<T>(HttpContent content)
        {
            using var utf8Json = await content.ReadAsStreamAsync()
                .ConfigureAwait(false);

            return await JsonSerializer.DeserializeAsync<T>(utf8Json, Defaults.JsonSerializerOptions).ConfigureAwait(false);
        }

        public Task<HttpContent> SerializeAsync<T>(T item)
        {
            var json = JsonSerializer.Serialize(item, Defaults.JsonSerializerOptions);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            return Task.FromResult((HttpContent)content);
        }
    }
}
