namespace Meeting.Events.Core
{
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    public class GetEventByUrlKeyQueryHandler : IRequestHandler<GetEventByUrlKeyQuery, GetEventByIdQueryResult>
    {
        private readonly IEventRepository _eventRepository;

        public GetEventByUrlKeyQueryHandler(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<GetEventByIdQueryResult> Handle(GetEventByUrlKeyQuery request, CancellationToken cancellationToken)
        {
            var @event = await _eventRepository.GetEventByUrlKey(request);

            if (@event == null)
                throw new EventNotFoundException();

            return @event;
        }
    }
}
