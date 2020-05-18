namespace Meeting.GatewayAPI
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Diagnostics;
    using Microsoft.AspNetCore.Http;
    using Refit;
    using System.Net;
    using System.Text.Json;

    public static class ApplicationBuilderExtensions
    {
        public static void UseExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    var code = context.Response.StatusCode;
                    var error = "There was an error.";

                    var errorFeature = context.Features.Get<IExceptionHandlerFeature>();

                    switch (errorFeature.Error)
                    {
                        case ApiException e:
                            code = (int)e.StatusCode;
                            error = e.Content;
                            break;
                    }

                    var json = JsonSerializer.Serialize(new ApiErrorResponse { Code = code, Error = error }, Defaults.JsonSerializerOptions);

                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = (int)code;

                    await context.Response.WriteAsync(json);
                });
            });
        }
    }
}
