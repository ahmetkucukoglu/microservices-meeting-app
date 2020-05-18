namespace Meeting.Groups.Core
{
    public class InvalidUrlKeyException: AggregateException
    {
        public InvalidUrlKeyException() : base("Invalid group url key.") { }
    }
}