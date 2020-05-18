namespace Meeting.Cities.Core
{
    public class InvalidLatitudeException: AggregateException
    {
        public InvalidLatitudeException() : base("Invalid city latitude.") { }
    }
}