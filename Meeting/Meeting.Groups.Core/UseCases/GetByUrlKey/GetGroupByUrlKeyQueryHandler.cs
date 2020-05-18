namespace Meeting.Groups.Core
{
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    public class GetGroupByUrlKeyQueryHandler : IRequestHandler<GetGroupByUrlKeyQuery, GetGroupByIdQueryResult>
    {
        private readonly IGroupRepository _groupRepository;

        public GetGroupByUrlKeyQueryHandler(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public async Task<GetGroupByIdQueryResult> Handle(GetGroupByUrlKeyQuery request, CancellationToken cancellationToken)
        {
            var group = await _groupRepository.GetGroupByUrlKey(request);

            if (group == null)
                throw new GroupNotFoundException();

            return group;
        }
    }
}
