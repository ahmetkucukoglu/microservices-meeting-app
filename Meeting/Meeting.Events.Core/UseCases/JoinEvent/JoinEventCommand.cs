namespace Meeting.Events.Core
{
    using MediatR;
    using System;

    public class JoinEventCommand : IRequest
    {
        public JoinEventCommand(Guid eventId, string attendeeId, DateTimeOffset joinedDate)
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
            JoinedDate = joinedDate;
        }

        public Guid EventId { get; }
        public string AttendeeId { get; }
        public DateTimeOffset JoinedDate { get; }
    }
}
