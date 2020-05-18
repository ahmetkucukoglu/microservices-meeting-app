namespace Meeting.Groups.Core
{
    using System;

    public class FindGroupsQueryResult
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string UrlKey { get; set; }
        public int MembersCount { get; set; }
    }
}
