namespace Meeting.Groups.Infrastructure
{
    using System;
    using System.Collections.Generic;

    public class GroupDocument
    {
        public GroupDocument()
        {
            Topics = new List<GroupTopicDocument>();
            Members = new List<GroupMemberDocument>();
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public string UrlKey { get; set; }
        public string OrganizerId { get; set; }
        public Guid CityId { get; set; }
        public List<GroupTopicDocument> Topics { get; set; }
        public List<GroupMemberDocument> Members { get; set; }
    }
}
