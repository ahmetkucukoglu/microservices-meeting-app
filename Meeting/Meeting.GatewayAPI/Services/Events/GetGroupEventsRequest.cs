namespace Meeting.GatewayAPI
{
    using System;

    public class GetGroupEventsRequest
    {
        public Guid GroupId { get; set; }
        public string Status { get; set; }
    }
}
