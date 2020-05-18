namespace Meeting.Cities.Core
{
    using System;

    public class GetCitiesByTermQueryResult
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public GetCitiesByTermLocationQueryResult Location { get; set; }
    }

    public class GetCitiesByTermLocationQueryResult
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
