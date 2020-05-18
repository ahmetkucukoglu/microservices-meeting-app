namespace Meeting.Events.Core
{
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    public class GetAttendeeInfoQueryHandler : IRequestHandler<GetAttendeeInfoQuery, GetAttendeeInfoQueryResult>
    {
        private readonly IEventRepository _eventRepository;

        public GetAttendeeInfoQueryHandler(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<GetAttendeeInfoQueryResult> Handle(GetAttendeeInfoQuery request, CancellationToken cancellationToken)
        {
            var @event = await _eventRepository.ExistsEvent(request.EventId);

            if (!@event)
                throw new EventNotFoundException();

            var result = await _eventRepository.GetAttendeeInfo(request);

            return result;
        }
    }
}
