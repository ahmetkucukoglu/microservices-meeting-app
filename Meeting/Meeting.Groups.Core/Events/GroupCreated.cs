namespace Meeting.Groups.Core
{
    using System;

    public partial class V1
    {
        public class GroupCreated : Event
        {
            public GroupCreated(Guid groupId, string groupName, string organizerId)
            {
                GroupId = groupId;
                GroupName = groupName;
                OrganizerId = organizerId;
            }

            public Guid GroupId { get; }
            public string GroupName { get; }
            public string OrganizerId { get; }
        }
    }
}
