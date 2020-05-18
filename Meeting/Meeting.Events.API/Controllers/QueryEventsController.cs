namespace Meeting.Events.API
{
    using MediatR;
    using Meeting.Events.Core;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading;
    using System.Threading.Tasks;

    [Route("api")]
    [ApiController]
    [Authorize]
    public class QueryEventsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public QueryEventsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("upcoming-events")]
        [AllowAnonymous]
        public async Task<IActionResult> UpcomingEvents([FromQuery] GetUpcomingEventsRequest request, CancellationToken cancellationToken)
        {
            var @events = await _mediator.Send(new GetUpcomingEventsQuery(request.Latitude, request.Longitude, request.Radius, request.Keyword), cancellationToken);

            return Ok(@events);
        }

        [HttpGet]
        [Route("group-events")]
        [AllowAnonymous]
        public async Task<IActionResult> GroupEvents([FromQuery] GetGroupEventsRequest request, CancellationToken cancellationToken)
        {
            var @events = await _mediator.Send(new GetGroupEventsQuery(request.GroupId, request.Status), cancellationToken);

            return Ok(@events);
        }
    }
}