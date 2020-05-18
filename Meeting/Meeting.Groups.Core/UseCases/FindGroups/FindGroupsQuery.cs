namespace Meeting.Groups.Core
{
    using MediatR;
    using System;
    using System.Collections.Generic;

    public class FindGroupsQuery : IRequest<IEnumerable<FindGroupsQueryResult>>
    {
        public FindGroupsQuery(Guid cityId, List<Guid> topicIds)
        {
            if (cityId == null)
            {
                throw new InvalidCityIdException();
            }

            if (topicIds == null || topicIds.Count == 0)
            {
                throw new InvalidTopicIdsException();
            }

            CityId = cityId;
            TopicIds = topicIds;
        }

        public Guid CityId { get; }
        public List<Guid> TopicIds { get; }
    }
}
