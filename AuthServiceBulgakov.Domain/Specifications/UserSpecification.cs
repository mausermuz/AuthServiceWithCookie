using AuthServiceBulgakov.Domain.Entites;
using NSpecifications;

namespace AuthServiceBulgakov.Domain.Specifications
{
    public static class UserSpecification
    {
        public static Spec<User> ByIds(Guid[] ids)
            => new Spec<User>(x => ids.Contains(x.Id));

        public static Spec<User> ByUserName(string userName)
            => new Spec<User>(x => x.UserName == userName);
        
        public static Spec<User> ByRefreshToken(string refreshTokenId)
            => new Spec<User>(x => x.RefreshToken.Token == refreshTokenId);

        public static Spec<User> ByPassword(string passwordHash)
            => new Spec<User>(x => x.PasswordHash == passwordHash);

        public static Spec<User> ByIsActive(bool isActive)
            => new Spec<User>(x => x.IsActive == isActive);
    }
}
