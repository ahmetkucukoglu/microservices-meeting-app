namespace Meeting.Events.Core
{
    public class EventNotFoundException : AggregateNotFoundException
    {
        public EventNotFoundException() : base("Event not found.") { }
    }
}
