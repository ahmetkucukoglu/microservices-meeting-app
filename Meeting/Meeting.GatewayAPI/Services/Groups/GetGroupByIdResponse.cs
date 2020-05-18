namespace Meeting.GatewayAPI
{
    using System;
    using System.Collections.Generic;

    public class GetGroupByIdResponse
    {
        public GetGroupByIdResponse()
        {
            Topics = new List<GetGroupByIdTopicResponse>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int MembersCount { get; set; }
        public string UrlKey { get; set; }
        public GetGroupByIdOrganizerResponse Organizer { get; set; }
        public GetGroupByIdCityResponse City { get; set; }
        public IEnumerable<GetGroupByIdTopicResponse> Topics { get; set; }
    }

    public class GetGroupByIdOrganizerResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class GetGroupByIdCityResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public class GetGroupByIdTopicResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
