namespace Meeting.Topics.API
{
    using Consul;
    using Couchbase.Extensions.DependencyInjection;
    using EventStore.ClientAPI;
    using MediatR;
    using Meeting.Topics.Core;
    using Meeting.Topics.Infrastructure;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.HttpOverrides;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.IdentityModel.Tokens;
    using Microsoft.OpenApi.Models;
    using System.Reflection;
    using System.Security.Claims;

    public static class ServiceCollectionExtensions
    {
        public static void AddEventStore(this IServiceCollection services, IConfiguration configuration)
        {
            var eventStoreConfig = configuration.GetSection("EventStore").Get<EventStoreConfig>();

            var userCrendetials = new global::EventStore.ClientAPI.SystemData.UserCredentials(eventStoreConfig.Username, eventStoreConfig.Password);

            var connectionSettings = ConnectionSettings.Create();
            connectionSettings.SetDefaultUserCredentials(userCrendetials);
            connectionSettings.KeepReconnecting();

            var eventStoreConnection = EventStoreConnection.Create(
                connectionSettings: connectionSettings,
                uri: eventStoreConfig.Host,
                connectionName: "Meeting.Topics.API");

            eventStoreConnection.ConnectAsync().GetAwaiter().GetResult();

            services.AddSingleton(eventStoreConnection);

            services.AddTransient<IEventStore, EventStore>();
        }

        public static void AddCouchbase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCouchbase((opt) =>
            {
                var couchbaseConfig = configuration.GetSection("Couchbase").Get<CouchbaseConfig>();

                opt.ConnectionString = couchbaseConfig.Host;
                opt.Username = couchbaseConfig.Username;
                opt.Password = couchbaseConfig.Password;
            })
               .AddCouchbaseBucket<ITopicsBucketProvider>("topics");

            services.AddTransient<ITopicRepository, TopicRepository>();
        }

        public static void AddConsul(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ConsulConfig>(configuration.GetSection("Consul"));

            services.AddSingleton<IHostedService, ServiceDiscoveryHostedService>();
            services.AddSingleton<IConsulClient, ConsulClient>(p => new ConsulClient(config =>
            {
                var consulConfig = configuration.GetSection("Consul").Get<ConsulConfig>();

                config.Address = consulConfig.ServiceDiscoveryAddress;
            }));
        }

        public static void AddMediatR(this IServiceCollection services)
        {
            services.AddMediatR(typeof(CreateTopicCommand).GetTypeInfo().Assembly);
        }

        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen((opt) =>
            {
                opt.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Topics API",
                    Version = "1.0"
                });
            });
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

        public static void AddForwardedHeaders(this IServiceCollection services)
        {
            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders = ForwardedHeaders.All;
            });
        }
    }
}
