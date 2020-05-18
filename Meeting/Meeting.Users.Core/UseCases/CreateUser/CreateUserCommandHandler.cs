namespace Meeting.Users.Core
{
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Unit>
    {
        private readonly IUserRepository _userRepository;
        private readonly IEventStore _eventStore;

        public CreateUserCommandHandler(IUserRepository userRepository, IEventStore eventStore)
        {
            _userRepository = userRepository;
            _eventStore = eventStore;
        }

        public async Task<Unit> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            await _userRepository.CreateUser(request.UserId, request);

            await _eventStore.SaveAsync<V1.UserCreated>(new V1.UserCreated[] { new V1.UserCreated(request.UserId, request.Name) });

            return Unit.Value;
        }
    }
}
