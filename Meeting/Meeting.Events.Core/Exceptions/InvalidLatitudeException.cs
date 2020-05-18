namespace Meeting.Events.Core
{
    public class InvalidLatitudeException: AggregateException
    {
        public InvalidLatitudeException() : base("Invalid event latitude.") { }
    }
}