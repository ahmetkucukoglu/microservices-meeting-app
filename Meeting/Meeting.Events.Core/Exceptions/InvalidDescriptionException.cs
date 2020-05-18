namespace Meeting.Events.Core
{
    public class InvalidDescriptionException: AggregateException
    {
        public InvalidDescriptionException() : base("Invalid event description.") { }
    }
}