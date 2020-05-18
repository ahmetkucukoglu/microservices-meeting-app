namespace Meeting.Events.Core
{
    public class InvalidGroupIdException : AggregateException
    {
        public InvalidGroupIdException() : base("Invalid group id.") { }
    }
}