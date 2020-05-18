namespace Meeting.Topics.Core
{
    public class InvalidNameException: AggregateException
    {
        public InvalidNameException() : base("Invalid topic name.") { }
    }
}