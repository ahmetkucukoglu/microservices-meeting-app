namespace Meeting.Events.Core
{
    public class InvalidSubjectException: AggregateException
    {
        public InvalidSubjectException() : base("Invalid event subject.") { }
    }
}