namespace Meeting.Events.Core
{
    public class CapacityLimitExceededException : AggregateException
    {
        public CapacityLimitExceededException() : base("Capacity limit exceeded.") { }
    }
}
