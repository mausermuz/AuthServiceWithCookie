using AuthServiceBulgakov.Api.Data;

namespace AuthServiceBulgakov.Api.Services
{
    public class RefreshTokenService
    {
        private readonly ApplicationDbContext _context;

        public RefreshTokenService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void SaveRefreshToken(RefreshToken refreshToken, ApplicationUser user)
        {
            user.RefreshToken = refreshToken;
            _context.SaveChanges();
        }

        public string GetStoredRefreshToken(ApplicationUser user)
        {
            return user.RefreshToken.Token;
        }
    }
}