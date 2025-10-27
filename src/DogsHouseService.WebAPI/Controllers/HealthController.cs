using DogsHouseService.WebAPI.Extensions;
using DogsHouseService.WebAPI.Swagger;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using System.Net.Mime;

namespace DogsHouseService.WebAPI.Controllers
{
    [EnableRateLimiting(RateLimitingExtensions.DefaultPolicyName)]
    [ApiController]
    public class HealthController : ControllerBase
    {
        /// <summary>
        /// Returns the current version of the Dogshouse service.
        /// </summary>
        /// <remarks>
        /// Example request:
        /// 
        ///     GET /ping
        ///     
        /// Example response:
        ///
        ///     "Dogshouseservice.Version1.0.1"
        /// </remarks>
        /// <returns>Plain text string with the service version.</returns>
        /// <response code="200">Returns the service version string.</response>
        /// <response code="429" >You have exceeded the allowed number of requests. Please try again later.</response>
        /// <response code="500">Internal Server Error.</response>
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
