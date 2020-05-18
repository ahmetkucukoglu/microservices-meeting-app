namespace Meeting.GatewayAPI
{
    using System.Net;
    using System.Net.Http;
    using System.Text.Json;
    using System.Threading;
    using System.Threading.Tasks;

    public class SuccessResponseHandler : DelegatingHandler
    {
        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(JsonSerializer.Serialize(new ApiSuccessResponse<object>
                    {
                        Code = (int)response.StatusCode,
                        Result = content.Length > 0 ? JsonSerializer.Deserialize<object>(content, Defaults.JsonSerializerOptions) : "OK"
                    }, Defaults.JsonSerializerOptions))
                };
            }

            return response;
        }
    }
}
