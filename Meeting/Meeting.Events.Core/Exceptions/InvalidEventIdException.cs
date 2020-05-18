namespace Meeting.Events.Core
{
    public class InvalidEventIdException: AggregateException
    {
        public InvalidEventIdException() : base("Invalid event id.") { }
    }
}