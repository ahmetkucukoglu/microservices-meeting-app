namespace Meeting.Events.Core
{
    public class EventNotCompletedException : AggregateException
    {
        public EventNotCompletedException() : base("Event is not completed.") { }
    }
}
