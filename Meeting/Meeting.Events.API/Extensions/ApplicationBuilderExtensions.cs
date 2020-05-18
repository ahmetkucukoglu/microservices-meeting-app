namespace Meeting.Events.API
{
    using Meeting.Events.Core;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Diagnostics;
    using Microsoft.AspNetCore.Http;
    using System.Net;

    public static class ApplicationBuilderExtensions
    {
        public static void UseExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    var code = HttpStatusCode.BadRequest;

                    var errorFeature = context.Features.Get<IExceptionHandlerFeature>();

                    if (errorFeature.Error is AggregateNotFoundException exception)
                    {
                        code = HttpStatusCode.NotFound;
                    }

                    context.Response.StatusCode = (int)code;

                    await context.Response.WriteAsync(errorFeature.Error.Message);
                });
            });
        }
    }
}
