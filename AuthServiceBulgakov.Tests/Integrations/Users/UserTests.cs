using AuthServiceBulgakov.Api.Contracts;
using AuthServiceBulgakov.Domain.Entites;
using AutoFixture;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace AuthServiceBulgakov.Tests.Integrations.Users
{
    public class UserTests : BaseIntegrationTest
    {
        private readonly IFixture _fixture;

        public UserTests(CustomWebApplicationFactory factory) : base(factory)
        {
            _fixture = new Fixture();
        }

        [Fact]
        public async Task GetUsers_Unauthorized_If_Without_Cookies()
        {
            // Act
            var response = await HttpClient.GetAsync("/api/user/getusers");

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task GetUsers_UserList_If_With_Cookies()
        {
            // Arrange & Act
            var loginModel = new LoginModel("admin", "admin");
            var content = new StringContent(JsonConvert.SerializeObject(loginModel), Encoding.UTF8, "application/json");
            var res = await HttpClient.PostAsync("/api/auth/login", content);

            res.Headers.TryGetValues("Set-Cookie", out var cookiesHeader);
            HttpClientSecond.DefaultRequestHeaders.Add("Cookie", cookiesHeader);
            // Act
            var response = await HttpClientSecond.GetAsync("/api/user/getusers");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var responseContent = await response.Content.ReadAsStringAsync();
            var users = JsonConvert.DeserializeObject<List<User>>(responseContent);
            
            Assert.NotNull(users);
            Assert.True(users.Any());
        }
    }
}
