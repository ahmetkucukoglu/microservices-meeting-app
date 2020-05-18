namespace Meeting.Groups.Consumer
{
    using MediatR;

    public class Notification : INotification
    {
        public object Event { get; set; }
    }
}
