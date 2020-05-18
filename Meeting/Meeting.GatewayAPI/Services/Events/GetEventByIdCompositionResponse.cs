namespace Meeting.GatewayAPI
{
    public class GetEventByIdCompositionResponse
    {
        public GetEventByIdResponse Event { get; set; }
        public GetGroupByIdResponse Group { get; set; }
        public GetAttendeeInfoResponse AttendeeInfo { get; set; }
    }
}
