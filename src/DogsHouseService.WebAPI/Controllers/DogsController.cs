using DogsHouseService.Application.UseCases.Dogs.Commands;
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

        #region Commands


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
