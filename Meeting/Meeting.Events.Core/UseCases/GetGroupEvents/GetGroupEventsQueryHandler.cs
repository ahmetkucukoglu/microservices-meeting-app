namespace Meeting.Events.Core
{
    using MediatR;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public class GetGroupEventsQueryHandler : IRequestHandler<GetGroupEventsQuery, IEnumerable<GetGroupEventsQueryResult>>
    {
        private readonly IEventRepository _eventRepository;
        private readonly IGroupRepository _groupRepository;

        public GetGroupEventsQueryHandler(IEventRepository eventRepository, IGroupRepository groupRepository)
        {
            _eventRepository = eventRepository;
            _groupRepository = groupRepository;
        }

        public async Task<IEnumerable<GetGroupEventsQueryResult>> Handle(GetGroupEventsQuery request, CancellationToken cancellationToken)
        {
            var isExistsGroup = await _groupRepository.ExistsGroup(request.GroupId);

            if (!isExistsGroup)
                throw new GroupNotFoundException();

            var events = await _eventRepository.GetGroupEvents(request);

            return events;
        }
    }
}
