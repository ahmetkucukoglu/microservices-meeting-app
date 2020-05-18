namespace Meeting.BlazorUI.Data
{
    using System;

    public class FindGroupsResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string UrlKey { get; set; }
        public int MembersCount { get; set; }
    }
}
