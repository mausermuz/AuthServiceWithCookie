using AuthServiceBulgakov.DataAccess.MSSQL;
using Microsoft.EntityFrameworkCore;

namespace AuthServiceBulgakov.Api.Middlewares
{
    public class CheckIsActiveUserMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CheckIsActiveUserMiddleware> _logger;

        public CheckIsActiveUserMiddleware(
            RequestDelegate next,
            ILogger<CheckIsActiveUserMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if(context.Request.Cookies.TryGetValue("accessToken", out var accessToken)
               && context.Request.Cookies.TryGetValue("refreshToken", out var refreshToken)
               && context.Request.Cookies.TryGetValue("username", out var userName))
            {
                var dbContext = context.RequestServices.GetRequiredService<ApplicationDbContext>();
                var isActiveUser = await dbContext.Users.AnyAsync(x => x.UserName == userName && x.IsActive);

                if (!isActiveUser)
                {
                    ClearCookies(context);
                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;

                    var messageDetail = new
                    {
                        Message = "Текущий пользователь является неактивным"
                    };

                    await context.Response.WriteAsJsonAsync(messageDetail);
                }
                else
                    await _next(context);
            }
            else
            {
                await _next(context);
            }
        }

        private void ClearCookies(HttpContext context)
        {
            foreach(var cookieKey in new[] {"accessToken", "refreshToken", "username", "isActive" })
                context.Response.Cookies.Delete(cookieKey);
        }
    }
}
