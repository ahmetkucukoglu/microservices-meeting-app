namespace Meeting.Events.Core
{
    public class InvalidCapacityException : AggregateException
    {
        public InvalidCapacityException() : base("Invalid event capacity.") { }
    }
}