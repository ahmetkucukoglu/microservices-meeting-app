namespace Meeting.Events.Core
{
    public class InvalidDateTodayException: AggregateException
    {
        public InvalidDateTodayException() : base("Date must greater than today.") { }
    }
}