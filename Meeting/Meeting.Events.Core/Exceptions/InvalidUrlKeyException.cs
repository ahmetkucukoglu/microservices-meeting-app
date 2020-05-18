namespace Meeting.Events.Core
{
    public class InvalidUrlKeyException: AggregateException
    {
        public InvalidUrlKeyException() : base("Invalid event url key.") { }
    }
}