namespace Meeting.Events.Core
{
    public class InvalidLongitudeException: AggregateException
    {
        public InvalidLongitudeException() : base("Invalid event longitude.") { }
    }
}