namespace Meeting.Events.API
{
    public class GetUpcomingEventsRequest
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Radius { get; set; }
        public string Keyword { get; set; }
    }
}
