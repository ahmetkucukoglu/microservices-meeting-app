namespace Meeting.Topics.API.Controllers
{
    using MediatR;
    using Meeting.Topics.Core;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading;
    using System.Threading.Tasks;

    [Route("api/topics")]
    [ApiController]
    [Authorize(Roles = "admin")]
    public class TopicsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TopicsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTopicRequest request, CancellationToken cancellationToken)
        {
            await _mediator.Send(new CreateTopicCommand(request.Name, request.Description), cancellationToken);

            return Ok();
        }

        [HttpGet]
        [Route("find")]
        [AllowAnonymous]
        public async Task<IActionResult> Find([FromQuery] GetTopicsByTermRequest request, CancellationToken cancellationToken)
        {
            var cities = await _mediator.Send(new GetTopicsByTermQuery(request.Term), cancellationToken);

            return Ok(cities);
        }
    }
}