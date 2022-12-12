using Infrastructure.RedisDb;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventSourceController : ControllerBase
    {
        private readonly IRedisManager _redisManager;

        public EventSourceController(IRedisManager redisManager)
        {
            _redisManager = redisManager;
        }

        [HttpGet()]
        public async Task<IActionResult> GetAll()
        {
            var result = await _redisManager.GetAllEventSource();
            return Ok(result);
        }
    }
}
