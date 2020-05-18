namespace Meeting.GatewayAPI.API
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [Route("api/events")]
    [ApiController]
    [Authorize]
    public class EventsController : ControllerBase
    {
        private readonly IEventsApi _eventsApi;
        private readonly IGroupsApi _groupsApi;

        public EventsController(IEventsApi eventsApi, IGroupsApi groupsApi)
        {
            _eventsApi = eventsApi;
            _groupsApi = groupsApi;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetEventsRequest request)
        {
            var events = await _eventsApi.GetAll(request);

            return Ok(events);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateEventRequest request)
        {
            var result = await _eventsApi.Create(request);

            return Ok(result);
        }

        [HttpGet]
        [Route("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(Guid id)
        {
            var tasks = new List<Task>();

            var @event = _eventsApi.GetById(id);
            tasks.Add(@event);

            Task<ApiSuccessResponse<GetAttendeeInfoResponse>> attendeeInfo = default;

            if (User.Identity.IsAuthenticated)
            {
                attendeeInfo = _eventsApi.AttendeeInfo(id);
                tasks.Add(attendeeInfo);
            }

            await Task.WhenAll(tasks);

            var group = await _groupsApi.GetById(@event.Result.Result.Group.Id);

            var response = new ApiSuccessResponse<GetEventByIdCompositionResponse>
            {
                Code = 200,
                Result = new GetEventByIdCompositionResponse
                {
                    Event = @event.Result.Result,
                    Group = group.Result,
                    AttendeeInfo = User.Identity.IsAuthenticated ? attendeeInfo.Result.Result : new GetAttendeeInfoResponse()
                }
            };

            return Ok(response);
        }

        [HttpGet]
        [Route("urlkey/{urlKey}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByUrlKey(string urlKey)
        {
            var tasks = new List<Task>();

            var @event = await _eventsApi.GetByUrlKey(urlKey);

            var group = _groupsApi.GetById(@event.Result.Group.Id);
            tasks.Add(group);

            Task<ApiSuccessResponse<GetAttendeeInfoResponse>> attendeeInfo = default;

            if (User.Identity.IsAuthenticated)
            {
                attendeeInfo = _eventsApi.AttendeeInfo(@event.Result.Id);
                tasks.Add(attendeeInfo);
            }

            await Task.WhenAll(tasks);

            var response = new ApiSuccessResponse<GetEventByIdCompositionResponse>
            {
                Code = 200,
                Result = new GetEventByIdCompositionResponse
                {
                    Event = @event.Result,
                    Group = group.Result.Result,
                    AttendeeInfo = User.Identity.IsAuthenticated ? attendeeInfo.Result.Result : new GetAttendeeInfoResponse()
                }
            };

            return Ok(response);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateEventRequest request)
        {
            var result = await _eventsApi.Update(id, request);

            return Ok(result);
        }

        [HttpGet]
        [Route("upcoming")]
        [AllowAnonymous]
        public async Task<IActionResult> Upcoming([FromQuery] GetUpcomingEventsRequest request)
        {
            var @events = await _eventsApi.Upcoming(request);

            return Ok(@events);
        }

        [HttpPatch]
        [Route("{id}/join")]
        public async Task<IActionResult> Join(Guid id)
        {
            var result = await _eventsApi.Join(id);

            return Ok(result);
        }

        [HttpPatch]
        [Route("{id}/leave")]
        public async Task<IActionResult> Leave(Guid id)
        {
            var result = await _eventsApi.Leave(id);

            return Ok(result);
        }

        [HttpPatch]
        [Route("{id}/complete")]
        public async Task<IActionResult> Complete(Guid id)
        {
            var result = await _eventsApi.Complete(id);

            return Ok(result);
        }

        [HttpPatch]
        [Route("{id}/cancel")]
        public async Task<IActionResult> Cancel(Guid id)
        {
            var result = await _eventsApi.Cancel(id);

            return Ok(result);
        }

        [HttpPost]
        [Route("{id}/comments")]
        public async Task<IActionResult> AddComment(Guid id, [FromBody] AddCommentRequest request)
        {
            var result = await _eventsApi.AddComment(id, request);

            return Ok(result);
        }

        [HttpGet]
        [Route("{id}/comments")]
        [AllowAnonymous]
        public async Task<IActionResult> Comments(Guid id)
        {
            var comments = await _eventsApi.Comments(id);

            return Ok(comments);
        }

        [HttpGet]
        [Route("{id}/attendees")]
        [AllowAnonymous]
        public async Task<IActionResult> Attendees(Guid id)
        {
            var attendees = await _eventsApi.Attendees(id);

            return Ok(attendees);
        }
    }
}