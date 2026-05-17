using LearnForge.Server.Api.Features.Users.Commands.CreateUserCommand;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LearnForge.Server.Api.Controllers.V1
{
    public class UsersController(IMediator mediator) : BaseController
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateUserCommand command)
        {
            return Ok(await mediator.Send(command));
        }
    }
}
