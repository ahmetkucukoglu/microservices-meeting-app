namespace Meeting.GatewayAPI
{
    using Refit;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IEventsApi
    {
        [Post("/events")]
        Task<ApiSuccessResponse<string>> Create([Body]CreateEventRequest request);

        [Get("/events/{id}")] 
        Task<ApiSuccessResponse<GetEventByIdResponse>> GetById(Guid id);

        [Get("/events/urlkey/{urlkey}")]
        Task<ApiSuccessResponse<GetEventByIdResponse>> GetByUrlKey(string urlkey);

        [Put("/events/{id}")]
        Task<ApiSuccessResponse<string>> Update(Guid id, [Body] UpdateEventRequest request);
        
        [Get("/upcoming-events")]
        Task<ApiSuccessResponse<List<GetUpcomingEventsResponse>>> Upcoming([Query] GetUpcomingEventsRequest request);

        [Get("/group-events")]
        Task<ApiSuccessResponse<List<GetGroupEventsResponse>>> GroupEvents([Query] GetGroupEventsRequest request);

        [Patch("/events/{id}/join")]
        Task<ApiSuccessResponse<string>> Join(Guid id);

        [Patch("/events/{id}/leave")]
        Task<ApiSuccessResponse<string>> Leave(Guid id);

        [Patch("/events/{id}/complete")]
        Task<ApiSuccessResponse<string>> Complete(Guid id);

        [Patch("/events/{id}/cancel")]
        Task<ApiSuccessResponse<string>> Cancel(Guid id);

        [Post("/events/{id}/comments")]
        Task<ApiSuccessResponse<string>> AddComment(Guid id, AddCommentRequest request);

        [Get("/events/{id}/comments")]
        Task<ApiSuccessResponse<IEnumerable<GetCommentsResponse>>> Comments(Guid id);

        [Get("/events/{id}/attendees")]
        Task<ApiSuccessResponse<IEnumerable<GetAttendeesResponse>>> Attendees(Guid id);

        [Get("/events/{id}/attendee-info")]
        Task<ApiSuccessResponse<GetAttendeeInfoResponse>> AttendeeInfo(Guid id);

        [Get("/events")]
        Task<ApiSuccessResponse<IEnumerable<GetEventsResponse>>> GetAll([Query] GetEventsRequest request);
    }
}
