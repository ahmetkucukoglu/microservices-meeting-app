namespace Meeting.Events.Core
{
    using MediatR;
    using System;
    using System.Collections.Generic;

    public class GetCommentsQuery : IRequest<IEnumerable<GetCommentsQueryResult>>
    {
        public GetCommentsQuery(Guid eventId)
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
