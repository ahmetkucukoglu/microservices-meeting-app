namespace Meeting.Events.Core
{
    public class CommentatorNotFoundException : AggregateException
    {
        public CommentatorNotFoundException() : base("Commentator not found.") { }
    }
}
