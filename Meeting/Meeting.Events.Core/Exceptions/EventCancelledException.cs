namespace Meeting.Events.Core
{
    public class EventCancelledException : AggregateException
    {
        public EventCancelledException() : base("Event is cancelled.") { }
    }
}
