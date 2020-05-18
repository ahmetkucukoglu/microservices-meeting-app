namespace Meeting.Events.Core
{
    using MediatR;
    using System;
    using System.Collections.Generic;

    public class GetGroupEventsQuery : IRequest<IEnumerable<GetGroupEventsQueryResult>>
    {
        public GetGroupEventsQuery(Guid groupId, string status)
        {
            if (groupId == null)
            {
                throw new InvalidGroupIdException();
            }

            if (string.IsNullOrEmpty(status) || !EventStatuses.Has(status))
            {
                throw new InvalidStatusException();
            }

            GroupId = groupId;
            Status = status;
        }

        public Guid GroupId { get; }
        public string Status { get; }
    }
}
