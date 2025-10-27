using DogsHouseService.WebAPI.Swagger;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace DogsHouseService.WebAPI.Controllers
{
    [ApiController]
    public class HealthController : ControllerBase
    {
        [HttpGet("ping")]
        [Produces(MediaTypeNames.Text.Plain)]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServerErrorResponseExample), StatusCodes.Status500InternalServerError)]
        public IActionResult Ping()
        {
            return Ok("Dogshouseservice.Version1.0.1");
        }
    }
}
