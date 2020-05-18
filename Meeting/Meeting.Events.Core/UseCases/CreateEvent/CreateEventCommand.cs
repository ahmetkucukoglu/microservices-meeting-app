namespace Meeting.Events.Core
{
    using MediatR;
    using System;

    public class CreateEventCommand : IRequest<Guid>
    {
        public CreateEventCommand(Guid groupId, string organizatorId, string subject, DateTimeOffset date, DateTimeOffset? endDate, int? capacity, string description, string address, double latitude, double longitude)
        {
            if (groupId == null)
            {
                throw new InvalidGroupIdException();
            }
            
            if (string.IsNullOrEmpty(subject))
            {
                throw new InvalidSubjectException();
            }

            if (string.IsNullOrEmpty(organizatorId))
            {
                throw new InvalidOrganizerIdException();
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

            GroupId = groupId;
            OrganizatorId = organizatorId;
            Subject = subject;
            UrlKey = getUrlKey();
            Date = date;
            EndDate = endDate;
            Capacity = capacity;
            Description = description;
            Address = address;
            Latitude = latitude;
            Longitude = longitude;
        }

        public Guid GroupId { get; }
        public string OrganizatorId { get; }
        public string Subject { get; }
        public string UrlKey { get; }
        public DateTimeOffset Date { get; }
        public DateTimeOffset? EndDate { get; }        
        public int? Capacity { get; }
        public string Description { get; }
        public string Address { get; }
        public double Latitude { get; }
        public double Longitude { get; }

        internal string getUrlKey()
        {
            var slugHelper = new SlugHelper();

            return slugHelper.GenerateSlug(Subject);
        }
    }
}
