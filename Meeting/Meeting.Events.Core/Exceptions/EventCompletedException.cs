namespace Meeting.Events.Core
{
    public class EventCompletedException : AggregateException
    {
        public EventCompletedException() : base("Event is completed.") { }
    }
}
