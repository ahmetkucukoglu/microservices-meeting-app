namespace Meeting.Groups.Core
{
    public class CityNotFoundException : AggregateException
    {
        public CityNotFoundException() : base("City not found.") { }
    }
}
