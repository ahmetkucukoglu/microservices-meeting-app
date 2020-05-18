namespace Meeting.Events.Infrastructure
{
    using global::EventStore.ClientAPI;

    public class CheckpointDocument
    {
        public string Key { get; set; }
        public Position Position { get; set; }
    }
}
