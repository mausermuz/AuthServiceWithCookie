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

                SetResponseCookies(response.AccessToken, response.UserName, response.RefreshToken, response.IsActive);
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
                return Unauthorized();

            var command = new RefreshTokenCommand(userName, refreshToken);
            var response = await mediator.Send(command);

            SetResponseCookies(response.AccessToken, response.UserName, response.RefreshToken, response.IsActive);
            return Ok();
        }
        
        private void SetResponseCookies(string accessToken, string userName, string refreshToken, bool isActive)
        {
            var cookieOption = new CookieOptions() 
            { 
                HttpOnly = true, 
                SameSite = SameSiteMode.Strict, 
                Expires = DateTime.Now.AddMinutes(_jwtSettings.MinutesToExpirationAccessToken), 
                Secure = true 
            };

            Response.Cookies.Append("accessToken", accessToken, cookieOption);
            Response.Cookies.Append("username", userName, cookieOption);
            Response.Cookies.Append("refreshToken", refreshToken, cookieOption);
            Response.Cookies.Append("isActive", isActive.ToString(), cookieOption);
        }
    }
}
