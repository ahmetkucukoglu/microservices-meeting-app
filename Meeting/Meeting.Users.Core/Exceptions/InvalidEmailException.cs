namespace Meeting.Users.Core
{
    public class InvalidEmailException : AggregateException
    {
        public InvalidEmailException() : base("Invalid user email.") { }
    }
}