using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.Application.User.Commands;
using UrlShortener.Application.ViewModels;

namespace UrlShortener.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("sign-up")]
        public async Task<ActionResult> SignUp([FromBody] CreateUserCommand command, CancellationToken cancellationToken)
        {
            var user = await _mediator.Send(command, cancellationToken);

            return Ok();
        }

        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("sign-in")]
        public async Task<ActionResult<TokenDto>> SignIn([FromBody] LoginUserCommand command, CancellationToken cancellationToken)
        {
            var token = await _mediator.Send(command, cancellationToken);

            return Ok(token);
        }

        [HttpPost("validate")]
        [Authorize]
        public IActionResult ValidateToken()
        {
            // If the request reaches here, the token is valid
            return Ok(new { valid = true, message = "Token is valid" });
        }

    }
}
