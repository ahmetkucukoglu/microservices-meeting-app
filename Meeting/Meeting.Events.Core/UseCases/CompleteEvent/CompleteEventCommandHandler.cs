namespace Meeting.Events.Core
{
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    public class CompleteEventCommandHandler : IRequestHandler<CompleteEventCommand, Unit>
    {
        private readonly IEventRepository _eventRepository;

        public CompleteEventCommandHandler(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<Unit> Handle(CompleteEventCommand request, CancellationToken cancellationToken)
        {
            var @event = await _eventRepository.GetEventById(new GetEventByIdQuery(request.EventId));

            if (@event == null)
                throw new EventNotFoundException();

            if (@event.Status == EventStatuses.CANCELLED)
                throw new EventCancelledException();

            if (@event.Group.OrganizerId != request.OrganizerId)
                throw new OrganizerNotFoundException();

            await _eventRepository.CompleteEvent(request);

            return Unit.Value;
        }
    }
}
