namespace Meeting.Events.Core
{
    using MediatR;
    using System;

    public class CompleteEventCommand : IRequest
    {
        public CompleteEventCommand(Guid eventId, string organizerId)
        {
            if (eventId == null)
            {
                throw new InvalidEventIdException();
            }

            if (string.IsNullOrEmpty(organizerId))
            {
                throw new InvalidOrganizerIdException();
            }

            EventId = eventId;
            OrganizerId = organizerId;
        }

        public Guid EventId { get; set; }
        public string OrganizerId { get; }
    }
}
