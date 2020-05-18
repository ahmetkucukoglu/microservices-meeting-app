namespace Meeting.Events.Core
{
    using MediatR;
    using System;
    using System.Collections.Generic;

    public class GetAttendeesQuery : IRequest<IEnumerable<GetAttendeesQueryResult>>
    {
        public GetAttendeesQuery(Guid eventId)
        {
            if (eventId == null)
            {
                throw new InvalidEventIdException();
            }

            EventId = eventId;
        }

        public Guid EventId { get; }
    }
}
