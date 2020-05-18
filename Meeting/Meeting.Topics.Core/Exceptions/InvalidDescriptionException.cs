namespace Meeting.Topics.Core
{
    public class InvalidDescriptionException : AggregateException
    {
        public InvalidDescriptionException() : base("Invalid topic description.") { }
    }
}