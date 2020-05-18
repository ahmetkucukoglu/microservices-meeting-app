namespace Meeting.Events.Core
{
    using MediatR;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, Guid>
    {
        private readonly IEventRepository _eventRepository;
        private readonly IGroupRepository _groupRepository;

        public CreateEventCommandHandler(IEventRepository eventRepository, IGroupRepository groupRepository)
        {
            _eventRepository = eventRepository;
            _groupRepository = groupRepository;
        }

        public async Task<Guid> Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {
            if (request.Date <= DateTimeOffset.Now.Date)
                throw new InvalidDateTodayException();

            var isExistsGroup = await _groupRepository.ExistsGroup(request.GroupId, request.OrganizatorId);

            if (!isExistsGroup)
                throw new GroupNotFoundException();

            var eventId = Guid.NewGuid();

            await _eventRepository.CreateEvent(eventId, request);

            return eventId;
        }
    }
}
