namespace Meeting.Events.Core
{
    using MediatR;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public class GetEventsQueryHandler : IRequestHandler<GetEventsQuery, IEnumerable<GetEventsQueryResult>>
    {
        private readonly IEventRepository _eventRepository;
        private readonly IGroupRepository _groupRepository;

        public GetEventsQueryHandler(IEventRepository eventRepository, IGroupRepository groupRepository)
        {
            _eventRepository = eventRepository;
            _groupRepository = groupRepository;
        }

        public async Task<IEnumerable<GetEventsQueryResult>> Handle(GetEventsQuery request, CancellationToken cancellationToken)
        {
            var isExistsGroup = await _groupRepository.ExistsGroup(request.GroupId, request.OrganizerId);

            if (!isExistsGroup)
                throw new GroupNotFoundException();

            var events = await _eventRepository.GetEvents(request);

            return events;
        }
    }
}
