using AuthServiceBulgakov.Domain.Entites;
using System.Security.Claims;

namespace AuthServiceBulgakov.Application.Services
{
    public interface IJwtTokenService
    {
        string GenerateAccessToken(User user);
    }
}
