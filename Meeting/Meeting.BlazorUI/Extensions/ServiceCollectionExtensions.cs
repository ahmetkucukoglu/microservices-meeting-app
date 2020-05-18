namespace Meeting.BlazorUI.Extensions
{
    using Meeting.BlazorUI.Data;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Authentication.OpenIdConnect;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.IdentityModel.Protocols.OpenIdConnect;
    using Microsoft.IdentityModel.Tokens;
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;

    public static class ServiceCollectionExtensions
    {
        public static void AddHttpClient(this IServiceCollection services, IConfiguration configuration)
        {
            var gatewayConfig = configuration.GetSection("Gateway").Get<GatewayConfig>();

            services.AddScoped<HttpClient>((x) =>
            {
                var httpClient = new HttpClient
                {
                    BaseAddress = new Uri($"{gatewayConfig.BaseAddress}/api/")
                };

                var httpContext = x.GetRequiredService<IHttpContextAccessor>();

                if (httpContext.HttpContext.User.Identity.IsAuthenticated)
                {
                    var accessToken = httpContext.HttpContext.GetTokenAsync("access_token").GetAwaiter().GetResult();
                    var tokenType = httpContext.HttpContext.GetTokenAsync("token_type").GetAwaiter().GetResult();

                    httpClient.DefaultRequestHeaders.Add("Authorization", $"{tokenType} {accessToken}");
                }

                return httpClient;
            });
        }

        public static void AddAuth(this IServiceCollection services, IConfiguration configuration)
        {
            var authConfig = configuration.GetSection("Auth0").Get<Auth0Config>();

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
                .AddCookie()
                .AddOpenIdConnect("Auth0", options =>
                {
                    options.Authority = $"https://{authConfig.Domain}";

                    options.ClientId = authConfig.ClientId;
                    options.ClientSecret = authConfig.ClientSecret;

                    options.ResponseType = OpenIdConnectResponseType.Code;
                    options.SaveTokens = true;

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        NameClaimType = "nickname",
                        RoleClaimType = "https://schemas.meeting.com/roles"
                    };

                    options.Scope.Add("openid");
                    options.Scope.Add("offline_access");

                    options.CallbackPath = new PathString("/signin-oidc");

                    options.ClaimsIssuer = "Auth0";

                    options.Events = new OpenIdConnectEvents
                    {
                        OnRedirectToIdentityProviderForSignOut = (context) =>
                        {
                            var logoutUri = $"https://{authConfig.Domain}/v2/logout?client_id={authConfig.ClientId}";

                            var postLogoutUri = context.Properties.RedirectUri;

                            if (!string.IsNullOrEmpty(postLogoutUri))
                            {
                                if (postLogoutUri.StartsWith("/"))
                                {
                                    var request = context.Request;
                                    postLogoutUri = request.Scheme + "://" + request.Host + request.PathBase + postLogoutUri;
                                }

                                logoutUri += $"&returnTo={ Uri.EscapeDataString(postLogoutUri)}";
                            }

                            context.Response.Redirect(logoutUri);
                            context.HandleResponse();

                            return Task.CompletedTask;
                        },
                        OnRedirectToIdentityProvider = (context) =>
                        {
                            context.ProtocolMessage.SetParameter("audience", authConfig.Audience);

                            return Task.CompletedTask;
                        }
                    };
                });
        }
    }
}
