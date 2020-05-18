namespace Meeting.Users.Infrastructure
{
    using global::EventStore.ClientAPI;
    using Meeting.Users.Core;
    using System;
    using System.Linq;
    using System.Text;
    using System.Text.Json;
    using System.Threading.Tasks;

    public class EventStore : IEventStore
    {
        private readonly IEventStoreConnection _eventStore;

        public EventStore(IEventStoreConnection eventStore)
        {
            _eventStore = eventStore;
        }

        public async Task SaveAsync<T>(T[] events) where T : Event
        {
            if (!events.Any())
            {
                return;
            }

            var eventDatas = events.Select(@event => new EventData(
                    Guid.NewGuid(),
                    @event.GetType().Name,
                    true,
                    Encoding.UTF8.GetBytes(JsonSerializer.Serialize(@event)),
                    Encoding.UTF8.GetBytes(@event.GetType().FullName)))
                .ToArray();

            var result = await _eventStore.AppendToStreamAsync(GetStreamName(), ExpectedVersion.Any, eventDatas);
        }

        private string GetStreamName() => "Users";
    }
}
