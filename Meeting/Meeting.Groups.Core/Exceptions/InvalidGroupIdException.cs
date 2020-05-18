namespace Meeting.Groups.Core
{
    public class InvalidGroupIdException : AggregateException
    {
        public InvalidGroupIdException() : base("Invalid group id.") { }
    }
}