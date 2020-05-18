namespace Meeting.Cities.Core
{
    public class InvalidCityIdException : AggregateException
    {
        public InvalidCityIdException() : base("Invalid city id.") { }
    }
}