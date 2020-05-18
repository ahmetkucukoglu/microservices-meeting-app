namespace Meeting.Events.Core
{
    public static class EventStatuses
    {
        public const string ACTIVE = "active";
        public const string COMPLETED = "completed";
        public const string CANCELLED = "cancelled";

        public static bool Has(string status)
        {
            return status == ACTIVE || status == COMPLETED || status == CANCELLED;
        }
    }
}
