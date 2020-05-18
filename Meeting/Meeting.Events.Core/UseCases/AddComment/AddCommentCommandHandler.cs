namespace Meeting.Events.Core
{
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    public class AddCommentCommandHandler : IRequestHandler<AddCommentCommand, Unit>
    {
        private readonly IEventRepository _eventRepository;
        private readonly IUserRepository _userRepository;

        public AddCommentCommandHandler(IEventRepository eventRepository, IUserRepository userRepository)
        {
            _eventRepository = eventRepository;
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(AddCommentCommand request, CancellationToken cancellationToken)
        {
            var @event = await _eventRepository.GetEventById(new GetEventByIdQuery(request.EventId));
            var attendeeInfo = await _eventRepository.GetAttendeeInfo(new GetAttendeeInfoQuery(request.EventId, request.CommentatorId));

            if (@event == null)
                throw new EventNotFoundException();

            if (@event.Status == EventStatuses.CANCELLED)
                throw new EventCancelledException();

            if (@event.Status != EventStatuses.COMPLETED)
                throw new EventNotCompletedException();

            var isExistsCommentator = await _userRepository.ExistsUser(request.CommentatorId);

            if (!isExistsCommentator)
                throw new CommentatorNotFoundException();

            if (!attendeeInfo.AttendedIn)
                throw new AttendeeNotFoundException();

            await _eventRepository.AddComment(request);

            return Unit.Value;
        }
    }
}
