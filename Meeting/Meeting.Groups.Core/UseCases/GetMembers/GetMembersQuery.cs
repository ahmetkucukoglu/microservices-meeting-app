namespace Meeting.Groups.Core
{
    using MediatR;
    using System;
    using System.Collections.Generic;

    public class GetMembersQuery : IRequest<IEnumerable<GetMembersQueryResult>>
    {
        public GetMembersQuery(Guid groupId)
        {
            if (groupId == null)
            {
                throw new GroupNotFoundException();
            }

            GroupId = groupId;
        }

        public Guid GroupId { get; }
    }
}
