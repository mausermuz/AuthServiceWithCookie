using AuthServiceBulgakov.Api.Data;
using AuthServiceBulgakov.Api.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace AuthServiceBulgakov.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly JwtTokenService _jwtTokenService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JwtSettings _jwtSettings;

        public AuthController(
            ILogger<AuthController> logger, 
            IOptions<JwtSettings> jwtSettings,
            JwtTokenService jwtTokenService,
            UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager)
        {
            _logger = logger;
            _jwtSettings = jwtSettings.Value;
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtTokenService = jwtTokenService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginApi([FromBody] LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var signIn = await _signInManager.PasswordSignInAsync(model.userName, model.password, false, false);

                if (signIn.Succeeded)
                {
                    var user = await _userManager.FindByEmailAsync(model.password);

                    if (!user.IsActive)
                    {
                        return Unauthorized();
                    }

                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.NameIdentifier, user.Id)
                    };
                    var token = _jwtTokenService.GenerateAccessToken(claims);

                    user.RefreshToken = new RefreshToken();

                    await _userManager.UpdateAsync(user);

                    Response.Cookies.Append("accessToken", token, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict });
                    Response.Cookies.Append("username", user.UserName, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict });
                    Response.Cookies.Append("refreshToken", user.RefreshToken.Token, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict });

                    return Ok();
                }
                else
                {
                    return BadRequest(new { signIn.IsLockedOut, signIn.IsNotAllowed, signIn.RequiresTwoFactor });
                }
            }
            else
                return BadRequest(ModelState);
        }

        [HttpGet("refresh")]
        public async Task<IActionResult> Refresh()
        {
            if (!(Request.Cookies.TryGetValue("username", out var userName) && Request.Cookies.TryGetValue("refreshToken", out var refreshToken)))
                return BadRequest();

            var user = _userManager.Users.FirstOrDefault(i => i.UserName == userName && i.RefreshToken.Token == refreshToken);

            if (user == null)
                return BadRequest();

            var claims = new List<Claim>
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(ClaimTypes.NameIdentifier, user.Id)
                    };

            var token = _jwtTokenService.GenerateAccessToken(claims);

            user.RefreshToken = new RefreshToken();

            await _userManager.UpdateAsync(user);

            Response.Cookies.Append("accessToken", token, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict });
            Response.Cookies.Append("username", user.UserName, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict });
            Response.Cookies.Append("refreshToken", user.RefreshToken.Token, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict });

            return Ok();
        }

        //public void SetTokensInsideCookie(TokenDto tokenDto, HttpContext context)
        //{
        //    context.Response.Cookies.Append("accessToken", tokenDto.AccessToken,
        //        new CookieOptions
        //        {
        //            Expires = DateTimeOffset.UtcNow.AddMinutes(5),
        //            HttpOnly = true,
        //            IsEssential = true,
        //            Secure = true,
        //            SameSite = SameSiteMode.None
        //        });
        //    context.Response.Cookies.Append("refreshToken", tokenDto.RefreshToken,
        //        new CookieOptions
        //        {
        //            Expires = DateTimeOffset.UtcNow.AddDays(7),
        //            HttpOnly = true,
        //            IsEssential = true,
        //            Secure = true,
        //            SameSite = SameSiteMode.None
        //        });
        //}

        public record LoginModel (string userName, string password);
    }
}
