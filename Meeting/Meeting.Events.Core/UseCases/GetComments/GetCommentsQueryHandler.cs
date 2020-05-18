namespace Meeting.Events.Core
{
    using MediatR;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public class GetCommentsQueryHandler : IRequestHandler<GetCommentsQuery, IEnumerable<GetCommentsQueryResult>>
    {
        private readonly IEventRepository _eventRepository;

        public GetCommentsQueryHandler(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<IEnumerable<GetCommentsQueryResult>> Handle(GetCommentsQuery request, CancellationToken cancellationToken)
        {
            var isExistsEvent = await _eventRepository.ExistsEvent(request.EventId);

            if (!isExistsEvent)
                throw new EventNotFoundException();

            var comments = await _eventRepository.GetComments(request);

            return comments;
        }
    }
}
