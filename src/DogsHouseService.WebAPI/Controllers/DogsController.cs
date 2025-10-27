using DogsHouseService.Application.UseCases.Dogs.Commands.CreateDog;
using DogsHouseService.Application.UseCases.Dogs.DTO;
using DogsHouseService.Application.UseCases.Dogs.Queries.GetDogs;
using DogsHouseService.WebAPI.Factories;
using DogsHouseService.WebAPI.Swagger;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace DogsHouseService.WebAPI.Controllers
{
    [ApiController]
    public class DogsController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly ProblemDetailsFactory _problemDetailsFactory;

        public DogsController(
            ISender sender,
            ProblemDetailsFactory problemDetailsFactory)
        {
            _sender = sender;
            _problemDetailsFactory = problemDetailsFactory;
        }

        #region Queries


        /// <summary>
        /// Returns a list of dogs with optional sorting and pagination.
        /// </summary>
        /// <remarks>
        /// Example request:
        /// 
        ///     GET /dogs
        ///     GET /dogs?attribute=weight&amp;order=desc
        ///     GET /dogs?pageNumber=2&amp;pageSize=5
        /// 
        /// Example response:
        /// <code>
        /// [
        ///     {
        ///         "name": "Neo",
        ///         "color": "red&amp;amber",
        ///         "tail_length": 22,
        ///         "weight": 32
        ///     },
        ///     {
        ///         "name": "Jessy",
        ///         "color": "black&amp;white",
        ///         "tail_length": 7,
        ///         "weight": 14
        ///     }
        /// ]
        /// </code>
        /// </remarks>
        /// <param name="attribute">Column to sort by: name, color, tailLength, weight</param>
        /// <param name="order">Sort order: asc or desc</param>
        /// <param name="pageNumber">Page number (default 1)</param>
        /// <param name="pageSize">Page size (default 10)</param>
        /// <response code="200">Returns a paginated list of dogs</response>
        /// <response code="400">Invalid query parameters</response>
        /// <response code="500">Internal Server Error.</response>
        [HttpGet("dogs")]
        [Produces(MediaTypeNames.Application.Json, MediaTypeNames.Application.ProblemJson)]
        [ProducesResponseType(typeof(List<DogResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ClientErrorResponseExample), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ServerErrorResponseExample), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetDogs(
            [FromQuery] string? attribute,
            [FromQuery] string? order = "asc",
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var query = new GetDogsQuery(
                attribute, order, pageNumber, pageSize);

            var result = await _sender.Send(query);

            if (result.IsFailure)
            {
                return _problemDetailsFactory.GetProblemDetails(result);
            }

            return Ok(result.Value);
        }


        #endregion

        #region Commands


        /// <summary>
        /// Creates a new dog in the system.
        /// </summary>
        /// <remarks>
        /// Example request:
        /// 
        ///     POST /dog
        ///     {
        ///         "name": "Doggy",
        ///         "color": "red",
        ///         "tailLength": 173,
        ///         "weight": 33
        ///     }
        /// </remarks>
        /// <response code="201">Dog successfully created. Returns the new Dog ID (GUID).</response>
        /// <response code="400">Invalid input, duplicate dog name, or validation failure.</response>
        /// <response code="500">Internal Server Error.</response>
        [HttpPost("dog")]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json, MediaTypeNames.Application.ProblemJson)]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ClientErrorResponseExample), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ServerErrorResponseExample), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateDog(
            [FromBody] CreateDogCommand command)
        {
            var commandResult = await _sender.Send(command);

            if (commandResult.IsFailure)
            {
                return _problemDetailsFactory.GetProblemDetails(commandResult);
            }

            return Created(
                uri: string.Empty,
                value: commandResult.Value);
        }


        #endregion
    }
}
