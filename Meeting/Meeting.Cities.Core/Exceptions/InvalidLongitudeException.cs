namespace Meeting.Cities.Core
{
    public class InvalidLongitudeException: AggregateException
    {
        public InvalidLongitudeException() : base("Invalid city longitude.") { }
    }
}