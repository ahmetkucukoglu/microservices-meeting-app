namespace Meeting.Events.Core
{
    public class InvalidRadiusException : AggregateException
    {
        public InvalidRadiusException() : base("Invalid radius.") { }
    }
}