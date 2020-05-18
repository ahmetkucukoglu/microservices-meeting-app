namespace Meeting.Events.Core
{
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    public class GetEventByIdQueryHandler : IRequestHandler<GetEventByIdQuery, GetEventByIdQueryResult>
    {
        private readonly IEventRepository _eventRepository;

        public GetEventByIdQueryHandler(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<GetEventByIdQueryResult> Handle(GetEventByIdQuery request, CancellationToken cancellationToken)
        {
            var @event = await _eventRepository.GetEventById(request);

            if (@event == null)
                throw new EventNotFoundException();

            return @event;
        }
    }
}
