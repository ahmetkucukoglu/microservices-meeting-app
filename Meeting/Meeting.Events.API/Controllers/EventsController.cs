namespace Meeting.Events.API
{
    using MediatR;
    using Meeting.Events.Core;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    [Route("api/events")]
    [ApiController]
    [Authorize]
    public class EventsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EventsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetEventsRequest request, CancellationToken cancellationToken)
        {
            var events = await _mediator.Send(new GetEventsQuery(request.GroupId, User.Identity.Name), cancellationToken);

            return Ok(events);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateEventRequest request, CancellationToken cancellationToken)
        {
            await _mediator.Send(new CreateEventCommand(request.GroupId, User.Identity.Name, request.Subject, request.Date, request.EndDate, request.Capacity, request.Description, request.Address, request.Latitude, request.Longitude), cancellationToken);

            return Ok();
        }

        [HttpGet]
        [Route("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var @event = await _mediator.Send(new GetEventByIdQuery(id), cancellationToken);

            return Ok(@event);
        }

        [HttpGet]
        [Route("urlkey/{urlKey}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByUrlKey(string urlKey, CancellationToken cancellationToken)
        {
            var @event = await _mediator.Send(new GetEventByUrlKeyQuery(urlKey), cancellationToken);

            return Ok(@event);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateEventRequest request, CancellationToken cancellationToken)
        {
            await _mediator.Send(new UpdateEventCommand(id, User.Identity.Name, request.Subject, request.Date, request.EndDate, request.Capacity, request.Description, request.Address, request.Latitude, request.Longitude), cancellationToken);

            return Ok();
        }

        [HttpPatch]
        [Route("{id}/join")]
        public async Task<IActionResult> Join(Guid id, CancellationToken cancellationToken)
        {
            await _mediator.Send(new JoinEventCommand(id, User.Identity.Name, DateTimeOffset.Now), cancellationToken);

            return Ok();
        }

        [HttpPatch]
        [Route("{id}/leave")]
        public async Task<IActionResult> Leave(Guid id, CancellationToken cancellationToken)
        {
            await _mediator.Send(new LeaveEventCommand(id, User.Identity.Name), cancellationToken);

            return Ok();
        }

        [HttpPatch]
        [Route("{id}/complete")]
        public async Task<IActionResult> Complete(Guid id, CancellationToken cancellationToken)
        {
            await _mediator.Send(new CompleteEventCommand(id, User.Identity.Name), cancellationToken);

            return Ok();
        }

        [HttpPatch]
        [Route("{id}/cancel")]
        public async Task<IActionResult> Cancel(Guid id, CancellationToken cancellationToken)
        {
            await _mediator.Send(new CancelEventCommand(id, User.Identity.Name), cancellationToken);

            return Ok();
        }

        [HttpPost]
        [Route("{id}/comments")]
        public async Task<IActionResult> AddComment(Guid id, [FromBody] AddCommentRequest request, CancellationToken cancellationToken)
        {
            await _mediator.Send(new AddCommentCommand(id, User.Identity.Name, request.Comment, DateTimeOffset.Now), cancellationToken);

            return Ok();
        }

        [HttpGet]
        [Route("{id}/comments")]
        [AllowAnonymous]
        public async Task<IActionResult> Comments(Guid id, CancellationToken cancellationToken)
        {
            var comments = await _mediator.Send(new GetCommentsQuery(id), cancellationToken);

            return Ok(comments);
        }

        [HttpGet]
        [Route("{id}/attendees")]
        [AllowAnonymous]
        public async Task<IActionResult> Attendees(Guid id, CancellationToken cancellationToken)
        {
            var attendees = await _mediator.Send(new GetAttendeesQuery(id), cancellationToken);

            return Ok(attendees);
        }

        [HttpGet]
        [Route("{id}/attendee-info")]
        public async Task<IActionResult> AttendeeInfo(Guid id, CancellationToken cancellationToken)
        {
            var attendeeInfo = await _mediator.Send(new GetAttendeeInfoQuery(id, User.Identity.Name), cancellationToken);

            return Ok(attendeeInfo);
        }
    }
}