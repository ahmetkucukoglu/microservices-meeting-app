namespace Meeting.Groups.Core
{
    public class GroupNotFoundException : AggregateNotFoundException
    {
        public GroupNotFoundException() : base("Group not found.") { }
    }
}