using AuthServiceBulgakov.Api.Attributes;
using AuthServiceBulgakov.Application.Dto.Users;
using AuthServiceBulgakov.Application.UseCases.Users.Commands;
using AuthServiceBulgakov.Application.UseCases.Users.Queries;
using AuthServiceBulgakov.Domain.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthServiceBulgakov.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController(IMediator mediator) : ControllerBase
    {
        [HttpGet("[action]")]
        [HasRole(Roles.Admin)]
        public async Task<IActionResult> GetUsers()
        {
            var query = new GetAllUsersQuery();
            var result = await mediator.Send(query);

            return Ok(result);
        }

        [HttpPut("[action]")]
        [HasRole(Roles.Admin)]
        public async Task<IActionResult> ChangeStatusUsers([FromBody] ChangeStatusUsersDto[] request)
        {
            var command = new ChangeStatusUsersCommand(request);
            await mediator.Send(command);

            return Ok();
        }
    }
}
