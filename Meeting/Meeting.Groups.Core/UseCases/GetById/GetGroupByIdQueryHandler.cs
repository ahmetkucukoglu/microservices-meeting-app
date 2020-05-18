namespace Meeting.Groups.Core
{
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    public class GetGroupByIdQueryHandler : IRequestHandler<GetGroupByIdQuery, GetGroupByIdQueryResult>
    {
        private readonly IGroupRepository _groupRepository;

        public GetGroupByIdQueryHandler(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public async Task<GetGroupByIdQueryResult> Handle(GetGroupByIdQuery request, CancellationToken cancellationToken)
        {
            var group = await _groupRepository.GetGroupById(request);

            if (group == null)
                throw new GroupNotFoundException();

            return group;
        }
    }
}
