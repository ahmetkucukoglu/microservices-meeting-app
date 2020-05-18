namespace Meeting.Groups.Core
{
    using MediatR;
    using System;

    public class GetMemberInfoQuery : IRequest<GetMemberInfoQueryResult>
    {
        public GetMemberInfoQuery(Guid groupId, string memberId)
        {
            if (groupId == null)
            {
                throw new InvalidGroupIdException();
            }

            if (string.IsNullOrEmpty(memberId))
            {
                throw new InvalidMemberIdException();
            }

            GroupId = groupId;
            MemberId = memberId;
        }

        public Guid GroupId { get; }
        public string MemberId { get; }
    }
}
