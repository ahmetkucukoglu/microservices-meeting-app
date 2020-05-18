namespace Meeting.GatewayAPI
{
    using System;
    using System.Collections.Generic;

    public class FindGroupsRequest
    {
        public Guid CityId { get; set; }
        public List<Guid> TopicIds { get; set; }
    }
}
