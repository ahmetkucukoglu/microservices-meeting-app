namespace Meeting.Events.Core
{
    using MediatR;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public class GetAttendeesQueryHandler : IRequestHandler<GetAttendeesQuery, IEnumerable<GetAttendeesQueryResult>>
    {
        private readonly IEventRepository _eventRepository;

        public GetAttendeesQueryHandler(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<IEnumerable<GetAttendeesQueryResult>> Handle(GetAttendeesQuery request, CancellationToken cancellationToken)
        {
            var isExistsEvent = await _eventRepository.ExistsEvent(request.EventId);

            if (!isExistsEvent)
                throw new EventNotFoundException();

            var comments = await _eventRepository.GetAttendees(request);

            return comments;
        }
    }
}
