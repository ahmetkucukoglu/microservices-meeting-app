namespace Meeting.Groups.Core
{
    public class MemberNotFoundException : AggregateException
    {
        public MemberNotFoundException() : base("Member not found.") { }
    }
}
