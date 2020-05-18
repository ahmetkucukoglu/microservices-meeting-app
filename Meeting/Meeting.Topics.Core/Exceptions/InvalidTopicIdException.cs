namespace Meeting.Topics.Core
{
    public class InvalidTopicIdException : AggregateException
    {
        public InvalidTopicIdException() : base("Invalid topic id.") { }
    }
}