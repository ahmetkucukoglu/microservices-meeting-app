namespace Meeting.Events.Core
{
    public class OrganizerNotFoundException : AggregateException
    {
        public OrganizerNotFoundException() : base("Organizer not found.") { }
    }
}
