namespace Meeting.Users.Core
{
    public class InvalidNameException: AggregateException
    {
        public InvalidNameException() : base("Invalid user name.") { }
    }
}