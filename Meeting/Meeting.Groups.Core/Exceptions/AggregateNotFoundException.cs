namespace Meeting.Groups.Core
{
    public class AggregateNotFoundException : AggregateException
    {
        protected AggregateNotFoundException(string message) : base(message) { }
    }
}
