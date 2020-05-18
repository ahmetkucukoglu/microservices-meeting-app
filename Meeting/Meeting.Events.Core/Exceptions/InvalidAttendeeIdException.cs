namespace Meeting.Events.Core
{
    public class InvalidAttendeeIdException: AggregateException
    {
        public InvalidAttendeeIdException() : base("Invalid attendee id.") { }
    }
}