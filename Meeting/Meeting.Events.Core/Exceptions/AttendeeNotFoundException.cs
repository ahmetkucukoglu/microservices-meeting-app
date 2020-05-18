namespace Meeting.Events.Core
{
    public class AttendeeNotFoundException : AggregateException
    {
        public AttendeeNotFoundException() : base("Attendee not found.") { }
    }
}
