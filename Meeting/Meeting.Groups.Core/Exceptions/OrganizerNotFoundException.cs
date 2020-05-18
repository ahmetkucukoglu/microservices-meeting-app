namespace Meeting.Groups.Core
{
    public class OrganizerNotFoundException : AggregateException
    {
        public OrganizerNotFoundException() : base("Organizer not found.") { }
    }
}
