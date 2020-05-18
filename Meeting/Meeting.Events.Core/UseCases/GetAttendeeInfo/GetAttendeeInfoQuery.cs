namespace Meeting.Events.Core
{
    using MediatR;
    using System;

    public class GetAttendeeInfoQuery : IRequest<GetAttendeeInfoQueryResult>
    {
        public GetAttendeeInfoQuery(Guid eventId, string attendeeId)
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
