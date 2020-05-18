namespace Meeting.GatewayAPI
{
    using System;
    using System.Collections.Generic;

    public class CreateGroupRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid CityId { get; set; }
        public List<Guid> TopicIds { get; set; }
    }
}
