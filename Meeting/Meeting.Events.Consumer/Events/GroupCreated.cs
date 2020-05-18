namespace Meeting.Groups.Core
{
    using System;

    public partial class V1
    {
        public class GroupCreated
        {
            public Guid GroupId { get; set; }
            public string GroupName { get; set; }
            public string OrganizerId { get; set; }
        }
    }
}
