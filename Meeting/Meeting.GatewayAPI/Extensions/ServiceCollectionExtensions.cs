namespace Meeting.GatewayAPI
{
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.IdentityModel.Tokens;
    using Refit;
    using System;
    using System.Security.Claims;

    public static class ServiceCollectionExtensions
    {
        public static void AddCities(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient("cities", c =>
            {
                var fabioConfig = configuration.GetSection("Fabio").Get<FabioConfig>();

                c.BaseAddress = new Uri($"{fabioConfig.Host}/{fabioConfig.CitiesSource}/api/cities");
            })
                .AddTypedClient(c => RestService.For<ICitiesApi>(c, Defaults.RefitSettings))
                .AddHttpMessageHandler<SuccessResponseHandler>()
                .AddHeaderPropagation();
        }

        public static void AddTopics(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient("topics", c =>
            {
                var fabioConfig = configuration.GetSection("Fabio").Get<FabioConfig>();

                c.BaseAddress = new Uri($"{fabioConfig.Host}/{fabioConfig.TopicsSource}/api/topics");
            })
                .AddTypedClient(c => RestService.For<ITopicsApi>(c, Defaults.RefitSettings))
                .AddHttpMessageHandler<SuccessResponseHandler>()
                .AddHeaderPropagation();
        }

        public static void AddUsers(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient("users", c =>
            {
                var fabioConfig = configuration.GetSection("Fabio").Get<FabioConfig>();

                c.BaseAddress = new Uri($"{fabioConfig.Host}/{fabioConfig.UsersSource}/api/users");
            })
                .AddTypedClient(c => RestService.For<IUsersApi>(c, Defaults.RefitSettings))
                .AddHttpMessageHandler<SuccessResponseHandler>()
                .AddHeaderPropagation();
        }

        public static void AddGroups(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient("groups", c =>
            {
                var fabioConfig = configuration.GetSection("Fabio").Get<FabioConfig>();

                c.BaseAddress = new Uri($"{fabioConfig.Host}/{fabioConfig.GroupsSource}/api/groups");
            })
                .AddTypedClient(c => RestService.For<IGroupsApi>(c, Defaults.RefitSettings))
                .AddHttpMessageHandler<SuccessResponseHandler>()
                .AddHeaderPropagation();
        }

        public static void AddEvents(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient("events", c =>
            {
                var fabioConfig = configuration.GetSection("Fabio").Get<FabioConfig>();

                c.BaseAddress = new Uri($"{fabioConfig.Host}/{fabioConfig.EventsSource}/api");
            })
                .AddTypedClient(c => RestService.For<IEventsApi>(c, Defaults.RefitSettings))
                .AddHttpMessageHandler<SuccessResponseHandler>()
                .AddHeaderPropagation();
        }

        public static void AddAuth(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
               .AddJwtBearer(options =>
               {
                   var auth0Config = configuration.GetSection("Auth0").Get<Auth0Config>();

                   options.Authority = auth0Config.Authority;
                   options.Audience = auth0Config.Audience;
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       NameClaimType = ClaimTypes.NameIdentifier,
                       RoleClaimType = "https://schemas.meeting.com/roles"
                   };
               });
        }

        public static void AddHeaderPropagation(this IServiceCollection services)
        {
            services.AddHeaderPropagation(options =>
            {
                options.Headers.Add("Authorization");
            });
        }

        public static void AddCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins("*");
                });
            });
        }
    }
}
