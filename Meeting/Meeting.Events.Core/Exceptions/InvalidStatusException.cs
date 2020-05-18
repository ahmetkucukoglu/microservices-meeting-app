namespace Meeting.Events.Core
{
    public class InvalidStatusException : AggregateException
    {
        public InvalidStatusException() : base("Invalid status.") { }
    }
}