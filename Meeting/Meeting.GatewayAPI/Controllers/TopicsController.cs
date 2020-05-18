namespace Meeting.GatewayAPI.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    [Route("api/topics")]
    [ApiController]
    [Authorize(Roles = "admin")]
    public class TopicsController : ControllerBase
    {
        private readonly ITopicsApi _topicsApi;

        public TopicsController(ITopicsApi topicsApi)
        {
            _topicsApi = topicsApi;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTopicRequest request)
        {
            var result = await _topicsApi.Create(request);

            return Ok(result);
        }

        [HttpGet]
        [Route("find")]
        [AllowAnonymous]
        public async Task<IActionResult> Find([FromQuery] GetTopicsByTermRequest request)
        {
            var cities = await _topicsApi.Find(request);

            return Ok(cities);
        }
    }
}