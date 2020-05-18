namespace Meeting.Groups.Core
{
    public class InvalidCityIdException : AggregateException
    {
        public InvalidCityIdException() : base("Invalid city id.") { }
    }
}