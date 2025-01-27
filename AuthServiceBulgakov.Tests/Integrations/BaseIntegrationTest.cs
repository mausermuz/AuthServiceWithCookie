using AuthServiceBulgakov.DataAccess.MSSQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Web;

namespace AuthServiceBulgakov.Tests.Integrations
{
    public abstract class BaseIntegrationTest : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly IServiceScope _serviceScope;
        protected readonly HttpClient HttpClient;

        protected BaseIntegrationTest(CustomWebApplicationFactory factory)
        {
            _serviceScope = factory.Services.CreateScope();

            HttpClient = factory.CreateClient();

            var context = _serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            //context.Database.EnsureDeleted();
            context.Database.Migrate();
        }

        public List<Cookie> GetCookies(HttpResponseMessage message)
        {
            message.Headers.TryGetValues("Set-Cookie", out var cookiesHeader);
            var cookies = cookiesHeader.Select(cookieString => CreateCookie(cookieString)).ToList();
            return cookies;
        }

        private Cookie CreateCookie(string cookieString)
        {
            var properties = cookieString.Split(';', StringSplitOptions.TrimEntries);
            var name = properties[0].Split("=")[0];
            var value = properties[0].Split("=")[1];
            var path = properties[2].Replace("path=", "");
            var cookie = new Cookie(name, value, path)
            {
                Secure = properties.Contains("secure"),
                HttpOnly = properties.Contains("httponly"),
                Expires = DateTime.Parse(properties[1].Replace("expires=", ""))
            };
            return cookie;
        }
    }
}
