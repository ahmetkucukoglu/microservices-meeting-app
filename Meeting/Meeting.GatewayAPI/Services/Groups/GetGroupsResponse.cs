namespace Meeting.GatewayAPI
{
    using System;

    public class GetGroupsResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int MembersCount { get; set; }
    }
}
