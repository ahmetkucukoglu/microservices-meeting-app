namespace Meeting.Events.API
{
    using System;

    public class ConsulConfig
    {
        public Uri ServiceDiscoveryAddress { get; set; }
        public string ServiceName { get; set; }
        public Uri ServiceAddress { get; set; }
        public string ServiceId { get; set; }
    }
}
