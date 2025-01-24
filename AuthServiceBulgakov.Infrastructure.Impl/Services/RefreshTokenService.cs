using AuthServiceBulgakov.Application.Options;
using AuthServiceBulgakov.Application.Services;
using AuthServiceBulgakov.DataAccess.MSSQL;
using AuthServiceBulgakov.Domain.Entites;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;

namespace AuthServiceBulgakov.Infrastructure.Impl.Services
{
    public class RefreshTokenService(
        ApplicationDbContext dbContext,
        IOptions<JwtSettings> options) : IRefreshTokenService
    {
        private JwtSettings _jwtSettings = options.Value;

        public async void SaveRefreshToken(User user)
        {
            var token = GenerateRefreshToken();
            var exrireRefreshToken = DateTime.Now.AddDays(_jwtSettings.DaysToExpirationRefreshToken);

            var refreshToken = new RefreshToken(Guid.NewGuid(), token, exrireRefreshToken, user.Id);
            await dbContext.RefreshTokens.AddAsync(refreshToken);
        }

        public string GetStoredRefreshToken(User user)
        {
            return user.RefreshToken.Token;
        }

        private string GenerateRefreshToken()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(24));
        }
    }
}
