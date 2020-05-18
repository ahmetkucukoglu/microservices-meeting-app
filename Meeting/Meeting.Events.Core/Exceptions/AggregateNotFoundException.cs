namespace Meeting.Events.Core
{
    public class AggregateNotFoundException : AggregateException
    {
        protected AggregateNotFoundException(string message) : base(message) { }
    }
}
