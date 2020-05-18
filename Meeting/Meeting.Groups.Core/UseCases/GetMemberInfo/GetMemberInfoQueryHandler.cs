namespace Meeting.Groups.Core
{
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    public class GetMemberInfoQueryHandler : IRequestHandler<GetMemberInfoQuery, GetMemberInfoQueryResult>
    {
        private readonly IGroupRepository _groupRepository;

        public GetMemberInfoQueryHandler(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public async Task<GetMemberInfoQueryResult> Handle(GetMemberInfoQuery request, CancellationToken cancellationToken)
        {
            var group = await _groupRepository.ExistsGroup(request.GroupId);

            if (!group)
                throw new GroupNotFoundException();

            var result = await _groupRepository.GetMemberInfo(request);

            return result;
        }
    }
}
