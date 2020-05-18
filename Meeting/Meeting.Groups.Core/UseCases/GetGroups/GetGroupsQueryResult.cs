namespace Meeting.Groups.Core
{
    using System;

    public class GetGroupsQueryResult
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int MembersCount { get; set; }
    }
}
