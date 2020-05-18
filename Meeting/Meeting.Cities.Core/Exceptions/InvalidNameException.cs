namespace Meeting.Cities.Core
{
    public class InvalidNameException: AggregateException
    {
        public InvalidNameException() : base("Invalid city name.") { }
    }
}