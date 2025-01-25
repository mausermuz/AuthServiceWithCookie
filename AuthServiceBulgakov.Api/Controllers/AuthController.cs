using AuthServiceBulgakov.Api.Contracts;
using AuthServiceBulgakov.Application.Options;
using AuthServiceBulgakov.Application.UseCases.Users.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Annotations;

namespace AuthServiceBulgakov.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(
        IMediator mediator,
        IOptions<JwtSettings> options) : ControllerBase
    {
        private readonly JwtSettings _jwtSettings = options.Value;

        [HttpPost("[action]")]
        [SwaggerOperation("Авторизация")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if(ModelState.IsValid)
            {
                var command = new LoginCommand(model.UserName, model.Password);
                var response = await mediator.Send(command);

                Response.Cookies.Append("accessToken", response.AccessToken, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict, Expires = DateTime.Now.AddMinutes(_jwtSettings.MinutesToExpirationAccessToken) });
                Response.Cookies.Append("username", response.UserName, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict });
                Response.Cookies.Append("refreshToken", response.RefreshToken, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict, Expires = DateTime.Now.AddDays(_jwtSettings.DaysToExpirationRefreshToken) });
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("[action]")]
        [SwaggerOperation("Получение нового access и refresh токенов")]
        public async Task<IActionResult> Refresh()
        {
            if (!(Request.Cookies.TryGetValue("username", out var userName) && Request.Cookies.TryGetValue("refreshToken", out var refreshToken)))
                return BadRequest();

            var command = new RefreshTokenCommand(userName, refreshToken);
            var response = await mediator.Send(command);

            Response.Cookies.Append("accessToken", response.AccessToken, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict, Expires = DateTime.Now.AddMinutes(_jwtSettings.MinutesToExpirationAccessToken) });
            Response.Cookies.Append("username", response.UserName, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict });
            Response.Cookies.Append("refreshToken", response.RefreshToken, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict, Expires = DateTime.Now.AddDays(_jwtSettings.DaysToExpirationRefreshToken) });

            return Ok();
        }
    }
}
