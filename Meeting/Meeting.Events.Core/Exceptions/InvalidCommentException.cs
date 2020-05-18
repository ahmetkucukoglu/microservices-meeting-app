namespace Meeting.Events.Core
{
    public class InvalidCommentException: AggregateException
    {
        public InvalidCommentException() : base("Invalid event comment.") { }
    }
}