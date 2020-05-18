namespace Meeting.Groups.Infrastructure
{
    using Newtonsoft.Json;
    using System;

    public class GroupMemberDocument
    {
        [JsonProperty("memberId")]
        public string MemberId { get; set; }
    }
}
