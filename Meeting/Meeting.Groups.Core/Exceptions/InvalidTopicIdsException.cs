namespace Meeting.Groups.Core
{
    public class InvalidTopicIdsException : AggregateException
    {
        public InvalidTopicIdsException() : base("Invalid topic ids.") { }
    }
}