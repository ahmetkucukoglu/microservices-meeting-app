namespace Meeting.Groups.Core
{
    using MediatR;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public class FindGroupsQueryHandler : IRequestHandler<FindGroupsQuery, IEnumerable<FindGroupsQueryResult>>
    {
        private readonly IGroupRepository _groupRepository;

        public FindGroupsQueryHandler(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public async Task<IEnumerable<FindGroupsQueryResult>> Handle(FindGroupsQuery request, CancellationToken cancellationToken)
        {
            var groups = await _groupRepository.FindGroups(request);

            return groups;
        }
    }
}
