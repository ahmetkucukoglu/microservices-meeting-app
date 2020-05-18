namespace Meeting.Groups.Consumer
{
    using EventStore.ClientAPI;
    using MediatR;
    using Meeting.Groups.Infrastructure;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Text;
    using System.Text.Json;
    using System.Threading;
    using System.Threading.Tasks;

    public class TopicsEventStoreHostedService : IHostedService
    {
        private readonly IEventStoreConnection _eventStore;
        private readonly IMediator _mediator;
        private readonly CheckpointRepository _checkpointRepository;
        private readonly ILogger<CitiesEventStoreHostedService> _logger;
        private EventStoreStreamCatchUpSubscription _subscription;

        public TopicsEventStoreHostedService(IEventStoreConnection eventStore, IMediator mediator, CheckpointRepository checkpointRepository, ILogger<CitiesEventStoreHostedService> logger)
        {
            _eventStore = eventStore;
            _mediator = mediator;
            _checkpointRepository = checkpointRepository;
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var topicsLastCheckpoint = await _checkpointRepository.GetAsync("topics");

            var settings = new CatchUpSubscriptionSettings(
                maxLiveQueueSize: 10000,
                readBatchSize: 500,
                verboseLogging: false,
                resolveLinkTos: false,
                subscriptionName: "Groups-Topics");

            _subscription = _eventStore.SubscribeToStreamFrom(
                stream: "Topics",
                lastCheckpoint: topicsLastCheckpoint?.PreparePosition,
                settings: settings,
                eventAppeared: async (sub, @event) =>
                {
                    if (@event.OriginalEvent.EventType.StartsWith("$"))
                        return;

                    try
                    {
                        await _mediator.Publish(new Notification
                        {
                            Event = JsonSerializer.Deserialize(Encoding.UTF8.GetString(@event.OriginalEvent.Data), Type.GetType(Encoding.UTF8.GetString(@event.OriginalEvent.Metadata)))
                        });

                        await _checkpointRepository.SaveAsync("topics", @event.OriginalPosition.GetValueOrDefault());
                    }
                    catch (Exception exception)
                    {
                        _logger.LogError(exception, exception.Message);
                    }
                },
                liveProcessingStarted: (sub) =>
                {
                    _logger.LogInformation("{SubscriptionName} subscription started.", sub.SubscriptionName);
                },
                subscriptionDropped: (sub, subDropReason, exception) =>
                {
                    _logger.LogWarning("{SubscriptionName} dropped. Reason: {SubDropReason}.", sub.SubscriptionName, subDropReason);
                });
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _subscription.Stop();

            return Task.CompletedTask;
        }
    }
}
