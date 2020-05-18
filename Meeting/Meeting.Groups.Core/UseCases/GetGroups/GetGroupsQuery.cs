namespace Meeting.Groups.Core
{
    using MediatR;
    using System;
    using System.Collections.Generic;

    public class GetGroupsQuery : IRequest<IEnumerable<GetGroupsQueryResult>>
    {
        public GetGroupsQuery(string organizerId)
        {
            if (string.IsNullOrEmpty(organizerId))
            {
                throw new InvalidOrganizerIdException();
            }

            OrganizerId = organizerId;
        }

        public string OrganizerId { get; }
    }
}
