namespace Meeting.BlazorUI.Data
{
    using System;

    public class GetCitiesByTermResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public GetCitiesByTermLocationResponse Location { get; set; }
    }

    public class GetCitiesByTermLocationResponse
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
