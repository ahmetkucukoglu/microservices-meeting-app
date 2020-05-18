namespace Meeting.GatewayAPI.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    [Route("api/cities")]
    [ApiController]
    [Authorize(Roles = "admin")]
    public class CitiesController : ControllerBase
    {
        private readonly ICitiesApi _citiesApi;

        public CitiesController(ICitiesApi citiesApi)
        {
            _citiesApi = citiesApi;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCityRequest request)
        {
            var result = await _citiesApi.Create(request);

            return Ok(result);
        }

        [HttpGet]
        [Route("find")]
        [AllowAnonymous]
        public async Task<IActionResult> Find([FromQuery] GetCitiesByTermRequest request)
        {
            var cities = await _citiesApi.Find(request);

            return Ok(cities);
        }
    }
}