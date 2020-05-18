namespace Meeting.Events.Core
{
    public class AttendeeIdAlreadyExistsException : AggregateException
    {
        public AttendeeIdAlreadyExistsException() : base("Attendee already exists.") { }
    }
}
