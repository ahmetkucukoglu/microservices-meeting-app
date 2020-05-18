namespace Meeting.Events.Core
{
    public class InvalidDateException: AggregateException
    {
        public InvalidDateException() : base("Invalid event date.") { }
    }
}