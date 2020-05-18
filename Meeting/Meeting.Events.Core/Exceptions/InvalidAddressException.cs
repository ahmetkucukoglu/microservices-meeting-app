namespace Meeting.Events.Core
{
    public class InvalidAddressException: AggregateException
    {
        public InvalidAddressException() : base("Invalid event address.") { }
    }
}