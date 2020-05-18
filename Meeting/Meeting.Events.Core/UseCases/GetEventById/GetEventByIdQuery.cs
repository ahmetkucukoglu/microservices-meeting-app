namespace Meeting.Events.Core
{
    using MediatR;
    using System;

    public class GetEventByIdQuery : IRequest<GetEventByIdQueryResult>
    {
        public GetEventByIdQuery(Guid eventId)
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
