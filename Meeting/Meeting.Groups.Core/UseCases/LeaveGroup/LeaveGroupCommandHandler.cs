namespace Meeting.Groups.Core
{
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    public class LeaveGroupCommandHandler : IRequestHandler<LeaveGroupCommand, Unit>
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IUserRepository _userRepository;

        public LeaveGroupCommandHandler(IGroupRepository groupRepository, IUserRepository userRepository)
        {
            _groupRepository = groupRepository;
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(LeaveGroupCommand request, CancellationToken cancellationToken)
        {
            var group = await _groupRepository.ExistsGroup(request.GroupId);

            if (!group)
                throw new GroupNotFoundException();

            var isExistsMember = await _userRepository.ExistsUser(request.MemberId);

            if (!isExistsMember)
                throw new MemberNotFoundException();

            await _groupRepository.LeaveGroup(request);

            return Unit.Value;
        }
    }
}
