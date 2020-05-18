namespace Meeting.Groups.Core
{
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    public class JoinGroupCommandHandler : IRequestHandler<JoinGroupCommand, Unit>
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IUserRepository _userRepository;

        public JoinGroupCommandHandler(IGroupRepository groupRepository, IUserRepository userRepository)
        {
            _groupRepository = groupRepository;
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(JoinGroupCommand request, CancellationToken cancellationToken)
        {
            var group = await _groupRepository.ExistsGroup(request.GroupId);

            if (!group)
                throw new GroupNotFoundException();

            var isExistsMember = await _userRepository.ExistsUser(request.MemberId);

            if (!isExistsMember)
                throw new MemberNotFoundException();

            await _groupRepository.JoinGroup(request);

            return Unit.Value;
        }
    }
}
