namespace Meeting.Cities.Core
{
    public class InvalidCountryException: AggregateException
    {
        public InvalidCountryException() : base("Invalid city country.") { }
    }
}