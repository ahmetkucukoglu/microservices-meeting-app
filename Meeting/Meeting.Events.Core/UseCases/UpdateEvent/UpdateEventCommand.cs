namespace Meeting.Events.Core
{
    using MediatR;
    using System;

    public class UpdateEventCommand : IRequest
    {
        public UpdateEventCommand(Guid eventId, string organizerId, string subject, DateTimeOffset date, DateTimeOffset? endDate, int? capacity, string description, string address, double latitude, double longitude)
        {
            if (eventId == null)
            {
                throw new InvalidEventIdException();
            }

            if (string.IsNullOrEmpty(organizerId))
            {
                throw new InvalidOrganizerIdException();
            }

            if (string.IsNullOrEmpty(subject))
            {
                throw new InvalidSubjectException();
            }

            if (date == null)
            {
                throw new InvalidDateException();
            }

            if (capacity.HasValue && capacity < 1)
            {
                throw new InvalidCapacityException();
            }

            if (string.IsNullOrEmpty(description))
            {
                throw new InvalidDescriptionException();
            }

            if (string.IsNullOrEmpty(address))
            {
                throw new InvalidAddressException();
            }

            if (latitude == 0)
            {
                throw new InvalidLatitudeException();
            }

            if (longitude == 0)
            {
                throw new InvalidLongitudeException();
            }

            EventId = eventId;
            OrganizerId = organizerId;
            Subject = subject;
            Date = date;
            EndDate = endDate;
            Capacity = capacity;
            Description = description;
            Address = address;
            Latitude = latitude;
            Longitude = longitude;
        }

        public Guid EventId { get; set; }
        public string OrganizerId { get; }
        public string Subject { get; }
        public DateTimeOffset Date { get; }
        public DateTimeOffset? EndDate { get; }
        public int? Capacity { get; }
        public string Description { get; }
        public string Address { get; }
        public double Latitude { get; }
        public double Longitude { get; }
    }
}
