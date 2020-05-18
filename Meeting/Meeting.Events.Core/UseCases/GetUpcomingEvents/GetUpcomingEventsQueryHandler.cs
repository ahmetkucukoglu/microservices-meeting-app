namespace Meeting.Events.Core
{
    using MediatR;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public class GetUpcomingEventsQueryHandler : IRequestHandler<GetUpcomingEventsQuery, IEnumerable<GetUpcomingEventsQueryResult>>
    {
        private readonly IEventRepository _eventRepository;

        public GetUpcomingEventsQueryHandler(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<IEnumerable<GetUpcomingEventsQueryResult>> Handle(GetUpcomingEventsQuery request, CancellationToken cancellationToken)
        {
            var events = await _eventRepository.GetUpcomingEvents(request);

            return events;
        }
    }
}
