namespace Meeting.Events.Consumer
{
    using Couchbase.Extensions.DependencyInjection;
    using EventStore.ClientAPI;
    using MediatR;
    using Meeting.Events.Infrastructure;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using System.Reflection;

    public static class ServiceCollectionExtensions
    {
        public static void AddEventStore(this IServiceCollection services)
        {
            IConfiguration configuration;

            using (var serviceProvider = services.BuildServiceProvider())
            {
                configuration = serviceProvider.GetService<IConfiguration>();
            }

            var eventStoreConfig = configuration.GetSection("EventStore").Get<EventStoreConfig>();

            var userCrendetials = new global::EventStore.ClientAPI.SystemData.UserCredentials(eventStoreConfig.Username, eventStoreConfig.Password);

            var connectionSettings = ConnectionSettings.Create();
            connectionSettings.SetDefaultUserCredentials(userCrendetials);
            connectionSettings.KeepReconnecting();

            var eventStoreConnection = EventStoreConnection.Create(
                connectionSettings: connectionSettings,
                uri: eventStoreConfig.Host,
                connectionName: "Meeting.Events.Consumer");

            eventStoreConnection.ConnectAsync().GetAwaiter().GetResult();

            services.AddSingleton(eventStoreConnection);

            services.AddHostedService<GroupsEventStoreHostedService>();
            services.AddHostedService<UsersEventStoreHostedService>();
        }

        public static void AddCouchbase(this IServiceCollection services)
        {
            IConfiguration configuration;

            using (var serviceProvider = services.BuildServiceProvider())
            {
                configuration = serviceProvider.GetService<IConfiguration>();
            }

            services.AddCouchbase((opt) =>
            {
                var couchbaseConfig = configuration.GetSection("Couchbase").Get<CouchbaseConfig>();

                opt.ConnectionString = couchbaseConfig.Host;
                opt.Username = couchbaseConfig.Username;
                opt.Password = couchbaseConfig.Password;
            })
               .AddCouchbaseBucket<IRelationsBucketProvider>("events-relations");

            services.AddTransient<CheckpointRepository>();
        }

        public static void AddMediatR(this IServiceCollection services)
        {
            services.AddMediatR(typeof(Notification).GetTypeInfo().Assembly);
        }
    }
}
