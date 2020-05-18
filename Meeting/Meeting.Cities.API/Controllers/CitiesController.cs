namespace Meeting.Cities.API.Controllers
{
    using MediatR;
    using Meeting.Cities.Core;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading;
    using System.Threading.Tasks;

    [Route("api/cities")]
    [ApiController]
    [Authorize(Roles = "admin")]
    public class CitiesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CitiesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCityRequest request, CancellationToken cancellationToken)
        {
            await _mediator.Send(new CreateCityCommand(request.Name, request.City, request.Country, request.Latitude, request.Longitude), cancellationToken);

            return Ok();
        }

        [HttpGet]
        [Route("find")]
        [AllowAnonymous]
        public async Task<IActionResult> Find([FromQuery] GetCitiesByTermRequest request, CancellationToken cancellationToken)
        {
            var cities = await _mediator.Send(new GetCitiesByTermQuery(request.Term), cancellationToken);

            return Ok(cities);
        }
    }
}