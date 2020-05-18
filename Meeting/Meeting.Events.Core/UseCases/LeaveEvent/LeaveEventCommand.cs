namespace Meeting.Events.Core
{
    using MediatR;
    using System;

    public class LeaveEventCommand : IRequest
    {
        public LeaveEventCommand(Guid eventId, string attendeeId)
        {
            if (eventId == null)
            {
                throw new InvalidEventIdException();
            }

            if (string.IsNullOrEmpty(attendeeId))
            {
                throw new InvalidAttendeeIdException();
            }

            EventId = eventId;
            AttendeeId = attendeeId;
        }

        public Guid EventId { get; }
        public string AttendeeId { get; }
    }
}
