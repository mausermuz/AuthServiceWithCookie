using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AuthServiceBulgakov.Api.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //var hasher = new PasswordHasher<ApplicationUser>();
        }
    }

    public class ApplicationUser : IdentityUser
    {
        public bool IsActive { get; internal set; }
        public RefreshToken RefreshToken { get; set; }
    }

    public class RefreshToken
    {
        public string Token { get; internal set; }
        public DateTime CreationDate { get; internal set; }
        public DateTime ExpiryDate { get; internal set; }
        public string UserId { get; internal set; }
        public ApplicationUser User { get; internal set; }
    }
}
