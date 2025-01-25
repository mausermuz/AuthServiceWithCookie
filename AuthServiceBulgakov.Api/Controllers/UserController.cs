using AuthServiceBulgakov.Api.Filters;
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
        [HasRoles(Roles.Admin)]
        public async Task<IActionResult> GetUsers()
        {
            var query = new GetAllUsersQuery();
            var result = await mediator.Send(query);

            return Ok(result);
        }
    }
}
