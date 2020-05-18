namespace Meeting.Events.Core
{
    public class InvalidCommentatorIdException: AggregateException
    {
        public InvalidCommentatorIdException() : base("Invalid commentator id.") { }
    }
}