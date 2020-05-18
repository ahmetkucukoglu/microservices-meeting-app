namespace Meeting.Cities.Core
{
    public class InvalidCityException : AggregateException
    {
        public InvalidCityException() : base("Invalid city.") { }
    }
}