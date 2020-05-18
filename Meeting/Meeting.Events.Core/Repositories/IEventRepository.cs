namespace Meeting.Events.Core
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IEventRepository
    {
        Task<bool> ExistsEvent(Guid eventId);
        Task CreateEvent(Guid eventId, CreateEventCommand createEventCommand);
        Task UpdateEvent(UpdateEventCommand updateEventCommand);
        Task JoinEvent(JoinEventCommand joinEventCommand);
        Task LeaveEvent(LeaveEventCommand leaveEventCommand);
        Task CompleteEvent(CompleteEventCommand completeEventCommand);
        Task CancelEvent(CancelEventCommand cancelEventCommand);
        Task AddComment(AddCommentCommand addCommentCommand);
        Task<GetEventByIdQueryResult> GetEventById(GetEventByIdQuery getEventByIdQuery);
        Task<GetEventByIdQueryResult> GetEventByUrlKey(GetEventByUrlKeyQuery getEventByUrlKeyQuery);
        Task<IEnumerable<GetUpcomingEventsQueryResult>> GetUpcomingEvents(GetUpcomingEventsQuery getUpcomingEventsQuery);
        Task<IEnumerable<GetGroupEventsQueryResult>> GetGroupEvents(GetGroupEventsQuery getGroupEventsQuery);
        Task<IEnumerable<GetCommentsQueryResult>> GetComments(GetCommentsQuery getCommentsQuery);
        Task<IEnumerable<GetAttendeesQueryResult>> GetAttendees(GetAttendeesQuery getAttendeesQuery);
        Task<GetAttendeeInfoQueryResult> GetAttendeeInfo(GetAttendeeInfoQuery getAttendeeInfoQuery);
        Task<IEnumerable<GetEventsQueryResult>> GetEvents(GetEventsQuery getEventsQuery);
    }
}
