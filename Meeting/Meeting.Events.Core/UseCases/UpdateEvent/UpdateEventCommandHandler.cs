namespace Meeting.Events.Core
{
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    public class UpdateEventCommandHandler : IRequestHandler<UpdateEventCommand, Unit>
    {
        private readonly IEventRepository _eventRepository;

        public UpdateEventCommandHandler(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<Unit> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
        {
            var @event = await _eventRepository.GetEventById(new GetEventByIdQuery(request.EventId));

            if (@event == null)
                throw new EventNotFoundException();

            if (@event.Status == EventStatuses.CANCELLED)
                throw new EventCancelledException();

            if (@event.Status == EventStatuses.COMPLETED)
                throw new EventCompletedException();

            if (@event.Group.OrganizerId != request.OrganizerId)
                throw new OrganizerNotFoundException();

            await _eventRepository.UpdateEvent(request);

            return Unit.Value;
        }
    }
}
