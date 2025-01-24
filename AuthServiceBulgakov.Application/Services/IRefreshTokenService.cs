using AuthServiceBulgakov.Domain.Entites;

namespace AuthServiceBulgakov.Application.Services
{
    public interface IRefreshTokenService
    {
        void SaveRefreshToken(User user);

        string GetStoredRefreshToken(User user);

    }
}
