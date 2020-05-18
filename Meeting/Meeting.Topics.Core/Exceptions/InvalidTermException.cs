namespace Meeting.Topics.Core
{
    public class InvalidTermException : AggregateException
    {
        public InvalidTermException() : base("Invalid topic term.") { }
    }
}