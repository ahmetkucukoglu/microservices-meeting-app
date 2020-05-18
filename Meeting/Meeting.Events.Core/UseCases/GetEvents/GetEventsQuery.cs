namespace Meeting.Events.Core
{
    using MediatR;
    using System;
    using System.Collections.Generic;

    public class GetEventsQuery : IRequest<IEnumerable<GetEventsQueryResult>>
    {
        public GetEventsQuery(Guid groupId, string organizerId)
        {
            if (groupId == null)
            {
                throw new InvalidGroupIdException();
            }

            if (string.IsNullOrEmpty(organizerId))
            {
                throw new InvalidOrganizerIdException();
            }

            GroupId = groupId;
            OrganizerId = organizerId;
        }

        public Guid GroupId { get; }
        public string OrganizerId { get; }
    }
}
