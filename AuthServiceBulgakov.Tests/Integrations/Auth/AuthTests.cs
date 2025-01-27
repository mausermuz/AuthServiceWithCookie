using AuthServiceBulgakov.Api.Contracts;
using AutoFixture;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace AuthServiceBulgakov.Tests.Integrations.Auth
{
    public class AuthTests : BaseIntegrationTest
    {
        private readonly IFixture _fixture;
        public AuthTests(CustomWebApplicationFactory factory) : base(factory)
        {
            _fixture = new Fixture();
        }

        [Fact]
        public async Task Login_Success_If_Valid_Data()
        {
            // Arrange
            var cookieContainer = new CookieContainer();
            var loginModel = new LoginModel("admin", "admin");
            var content = new StringContent(JsonConvert.SerializeObject(loginModel), Encoding.UTF8, "application/json");

            // Act
            var response = await HttpClient.PostAsync("/api/auth/login", content);
            var cookies = GetCookies(response);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.True(cookies.Any());
        }

        [Fact]
        public async Task Login_Unsuccess_If_InValid_Data()
        {
            // Arrange
            var cookieContainer = new CookieContainer();
            var loginModel = new LoginModel("admin", "test");
            var content = new StringContent(JsonConvert.SerializeObject(loginModel), Encoding.UTF8, "application/json");

            // Act
            var response = await HttpClient.PostAsync("/api/auth/login", content);
            var responseContent = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Equal("We could not log you in. Please check your username/password and try again", responseContent);
        }

        [Fact]
        public async Task Refresh_Success_If_Valid_Cookies()
        {
            // Arrange&Act
            var cookieContainer = new CookieContainer();
            var loginModel = new LoginModel("admin", "admin");
            var content = new StringContent(JsonConvert.SerializeObject(loginModel), Encoding.UTF8, "application/json");
            var firstResponse = await HttpClient.PostAsync("/api/auth/login", content);

            // Act
            firstResponse.Headers.TryGetValues("Set-Cookie", out var cookiesHeader);
            HttpClientSecond.DefaultRequestHeaders.Add("Cookie", cookiesHeader);
            var response = await HttpClientSecond.GetAsync("/api/auth/refresh");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Refresh_Unsuccess_If_InValid_NotExists_Cookies()
        {
            // Act
            var response = await HttpClient.GetAsync("/api/auth/refresh");

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }
    }
}
