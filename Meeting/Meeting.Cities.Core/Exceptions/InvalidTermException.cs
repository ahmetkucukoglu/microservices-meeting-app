namespace Meeting.Cities.Core
{
    public class InvalidTermException : AggregateException
    {
        public InvalidTermException() : base("Invalid city term.") { }
    }
}