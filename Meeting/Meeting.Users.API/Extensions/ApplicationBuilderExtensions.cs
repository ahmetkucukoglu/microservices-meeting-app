namespace Meeting.Users.API
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Diagnostics;
    using Microsoft.AspNetCore.Http;

    public static class ApplicationBuilderExtensions
    {
        public static void UseExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    var errorFeature = context.Features.Get<IExceptionHandlerFeature>();

                    context.Response.StatusCode = 400;

                    await context.Response.WriteAsync(errorFeature.Error.Message);
                });
            });
        }
    }
}
