namespace Meeting.Groups.Core
{
    using MediatR;
    using System;

    public class GetGroupByIdQuery : IRequest<GetGroupByIdQueryResult>
    {
        public GetGroupByIdQuery(Guid groupId)
        {
            if (groupId == null)
            {
                throw new InvalidGroupIdException();
            }

            GroupId = groupId;
        }

        public Guid GroupId { get; }
    }
}
