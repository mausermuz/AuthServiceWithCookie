using AuthServiceBulgakov.Api.Contracts;
using AuthServiceBulgakov.Domain.Entites;
using AutoFixture;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
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
        public async Task GetUsers_Authorized_If_With_Cookies()
        {
            // Arrange
            var loginModel = new LoginModel("admin", "admin");
            var content = new StringContent(JsonConvert.SerializeObject(loginModel), Encoding.UTF8, "application/json");
            var res = await HttpClient.PostAsync("/api/auth/login", content);
            //var cookies = GetCookies(res);

            //var cookieContainer = new CookieContainer();
            //cookies.ForEach(c => cookieContainer.Add(c));
            //using (var handler = new HttpClientHandler() { CookieContainer = cookieContainer });
                       
            // Act
            //var response = await HttpClient.GetAsync("/api/user/getusers");

            // Assert
            //Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            //var responseContent = await response.Content.ReadAsStringAsync();
            //var users = JsonConvert.DeserializeObject<List<User>>(responseContent);
            
            //Assert.NotNull(users);
            //Assert.True(users.Any());
        }
    }
}
