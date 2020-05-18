namespace Meeting.GatewayAPI
{
    using System;

    public class GetCitiesByTermResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public GetCitiesByTermLocationQueryResponse Location { get; set; }
    }

    public class GetCitiesByTermLocationQueryResponse
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
