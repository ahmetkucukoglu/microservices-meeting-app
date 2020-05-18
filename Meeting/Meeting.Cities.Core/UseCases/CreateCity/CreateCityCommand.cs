namespace Meeting.Cities.Core
{
    using MediatR;

    public class CreateCityCommand : IRequest
    {
        public CreateCityCommand(string name, string city, string country, double latitude, double longitude)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new InvalidNameException();
            }

            if (string.IsNullOrEmpty(city))
            {
                throw new InvalidCityException();
            }

            if (string.IsNullOrEmpty(country))
            {
                throw new InvalidCountryException();
            }

            if (latitude == 0)
            {
                throw new InvalidLatitudeException();
            }

            if (longitude == 0)
            {
                throw new InvalidLongitudeException();
            }

            Name = name;
            City = city;
            Country = country;
            Latitude = latitude;
            Longitude = longitude;
        }

        public string Name { get; }
        public string City { get; }
        public string Country { get; }
        public double Latitude { get; }
        public double Longitude { get; }
    }
}
