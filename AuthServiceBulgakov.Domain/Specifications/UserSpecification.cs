using AuthServiceBulgakov.Domain.Entites;
using NSpecifications;

namespace AuthServiceBulgakov.Domain.Specifications
{
    public static class UserSpecification
    {
        public static Spec<User> ByIds(Guid[] ids)
        {
            return new Spec<User>(x => ids.Contains(x.Id));
        }

        public static Spec<User> ByUserName(string userName)
        {
            return new Spec<User>(x => x.UserName == userName);
        }
        
        public static Spec<User> ByRefreshToken(string refreshTokenId)
        {
            return new Spec<User>(x => x.RefreshToken.Token == refreshTokenId);
        }

        public static Spec<User> ByPassword(string passwordHash)
        {
            return new Spec<User>(x => x.PasswordHash == passwordHash);
        }
    }
}
