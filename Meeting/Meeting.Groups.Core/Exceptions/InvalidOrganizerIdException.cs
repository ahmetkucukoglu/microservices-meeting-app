namespace Meeting.Groups.Core
{
    public class InvalidOrganizerIdException: AggregateException
    {
        public InvalidOrganizerIdException() : base("Invalid organizer id.") { }
    }
}