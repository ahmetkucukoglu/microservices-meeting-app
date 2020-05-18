namespace Meeting.Topics.Core
{
    public class InvalidUrlKeyException: AggregateException
    {
        public InvalidUrlKeyException() : base("Invalid topic url key.") { }
    }
}