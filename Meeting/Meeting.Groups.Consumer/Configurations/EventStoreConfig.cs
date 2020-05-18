namespace Meeting.Groups.Consumer
{
    using System;

    public class EventStoreConfig
    {
        public Uri Host { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
