namespace Meeting.Events.Core
{
    using MediatR;
    using System.Collections.Generic;

    public class GetUpcomingEventsQuery : IRequest<IEnumerable<GetUpcomingEventsQueryResult>>
    {
        public GetUpcomingEventsQuery(double latitude, double longitude, string radius, string keywords)
        {
            if (latitude == 0)
            {
                throw new InvalidLatitudeException();
            }

            if (longitude == 0)
            {
                throw new InvalidLatitudeException();
            }

            if (string.IsNullOrEmpty(radius))
            {
                throw new InvalidRadiusException();
            }

            Latitude = latitude;
            Longitude = longitude;
            Radius = radius;
            Keywords = keywords;
        }

        public double Latitude { get; }
        public double Longitude { get; }
        public string Radius { get; }
        public string Keywords { get; }
    }
}
