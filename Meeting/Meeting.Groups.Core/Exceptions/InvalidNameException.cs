namespace Meeting.Groups.Core
{
    public class InvalidNameException : AggregateException
    {
        public InvalidNameException() : base("Invalid group name.") { }
    }
}