namespace Meeting.Events.Core
{
    public class GroupNotFoundException : AggregateException
    {
        public GroupNotFoundException() : base("Group not found.") { }
    }
}
