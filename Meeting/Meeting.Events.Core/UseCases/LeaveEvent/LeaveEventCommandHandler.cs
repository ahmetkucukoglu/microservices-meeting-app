namespace Meeting.Events.Core
{
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    public class LeaveEventCommandHandler : IRequestHandler<LeaveEventCommand, Unit>
    {
        private readonly IEventRepository _eventRepository;
        private readonly IUserRepository _userRepository;

        public LeaveEventCommandHandler(IEventRepository eventRepository, IUserRepository userRepository)
        {
            _eventRepository = eventRepository;
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(LeaveEventCommand request, CancellationToken cancellationToken)
        {
            var @event = await _eventRepository.GetEventById(new GetEventByIdQuery(request.EventId));

            if (@event == null)
                throw new EventNotFoundException();

            if (@event.Status == EventStatuses.CANCELLED)
                throw new EventCancelledException();

            if (@event.Status == EventStatuses.COMPLETED)
                throw new EventCompletedException();

            var isExistsAttendee = await _userRepository.ExistsUser(request.AttendeeId);

            if (!isExistsAttendee)
                throw new AttendeeNotFoundException();

            await _eventRepository.LeaveEvent(request);

            return Unit.Value;
        }
    }
}
