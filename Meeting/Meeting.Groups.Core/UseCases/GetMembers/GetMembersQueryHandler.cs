namespace Meeting.Groups.Core
{
    using MediatR;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public class GetMembersQueryHandler : IRequestHandler<GetMembersQuery, IEnumerable<GetMembersQueryResult>>
    {
        private readonly IGroupRepository _groupRepository;

        public GetMembersQueryHandler(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public async Task<IEnumerable<GetMembersQueryResult>> Handle(GetMembersQuery request, CancellationToken cancellationToken)
        {
            var isExistsGroup = await _groupRepository.ExistsGroup(request.GroupId);

            if (!isExistsGroup)
                throw new GroupNotFoundException();

            var members = await _groupRepository.GetMembers(request);

            return members;
        }
    }
}
