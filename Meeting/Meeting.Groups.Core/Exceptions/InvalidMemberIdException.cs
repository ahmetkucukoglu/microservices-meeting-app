namespace Meeting.Groups.Core
{
    public class InvalidMemberIdException : AggregateException
    {
        public InvalidMemberIdException() : base("Invalid member id.") { }
    }
}