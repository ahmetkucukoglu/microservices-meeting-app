namespace Meeting.Cities.Infrastructure
{
    public class CityDocument
    {
        public string Name { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public CityLocationDocument Location { get; set; }
    }
}
