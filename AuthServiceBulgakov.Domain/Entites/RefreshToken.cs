using AuthServiceBulgakov.Domain.Seedwork;

namespace AuthServiceBulgakov.Domain.Entites
{
    public class RefreshToken : BaseEntity
    {
        public string Token { get; private set; }
        public DateTime CreationDate { get; private set; }
        public DateTime ExpiryDate { get; private set; }
        public Guid UserId { get; private set; }
        public User? User { get; private set; }

        protected RefreshToken()
        {
            
        }
        public RefreshToken(Guid id, string token, DateTime expiriDate, Guid userId)
        {
            Id = id;
            Token = token;
            ExpiryDate = expiriDate;
            UserId = userId;
            CreationDate = DateTime.Now;
        }
    }
}
