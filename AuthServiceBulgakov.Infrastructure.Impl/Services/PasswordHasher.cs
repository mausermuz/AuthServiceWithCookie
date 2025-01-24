using AuthServiceBulgakov.Application.Services;

namespace AuthServiceBulgakov.Infrastructure.Impl.Services
{
    public class PasswordHasher : IPasswordHasher
    {
        public string GenerateHash(string password)
            => BCrypt.Net.BCrypt.HashPassword(password);

        public bool VerifyPassword(string password, string hashPassword)
            => BCrypt.Net.BCrypt.EnhancedVerify(password, hashPassword);
    }
}
