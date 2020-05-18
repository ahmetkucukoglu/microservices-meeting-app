namespace Meeting.Groups.Core
{
    using MediatR;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public class GetGroupsQueryHandler : IRequestHandler<GetGroupsQuery, IEnumerable<GetGroupsQueryResult>>
    {
        private readonly IGroupRepository _groupRepository;

        public GetGroupsQueryHandler(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public async Task<IEnumerable<GetGroupsQueryResult>> Handle(GetGroupsQuery request, CancellationToken cancellationToken)
        {
            var groups = await _groupRepository.GetGroups(request);

            return groups;
        }
    }
}
