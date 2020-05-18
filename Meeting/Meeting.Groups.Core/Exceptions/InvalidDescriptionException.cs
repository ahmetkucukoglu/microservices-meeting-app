namespace Meeting.Groups.Core
{
    public class InvalidDescriptionException : AggregateException
    {
        public InvalidDescriptionException() : base("Invalid group description.") { }
    }
}