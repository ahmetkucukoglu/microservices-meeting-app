namespace Meeting.Groups.Core
{
    public class TopicNotFoundException : AggregateException
    {
        public TopicNotFoundException() : base("Topic not found.") { }
    }
}
