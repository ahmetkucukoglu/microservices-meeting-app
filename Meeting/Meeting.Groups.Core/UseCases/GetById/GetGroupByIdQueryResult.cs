namespace Meeting.Groups.Core
{
    using System;
    using System.Collections.Generic;

    public class GetGroupByIdQueryResult
    {
        public GetGroupByIdQueryResult()
        {
            Topics = new List<GetGroupByIdTopicQueryResult>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string UrlKey { get; set; }
        public string Description { get; set; }
        public int MembersCount { get; set; }
        public GetGroupByIdOrganizerQueryResult Organizer { get; set; }
        public GetGroupByIdCityQueryResult City { get; set; }
        public IEnumerable<GetGroupByIdTopicQueryResult> Topics { get; set; }
    }

    public class GetGroupByIdOrganizerQueryResult
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class GetGroupByIdCityQueryResult
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public class GetGroupByIdTopicQueryResult
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
