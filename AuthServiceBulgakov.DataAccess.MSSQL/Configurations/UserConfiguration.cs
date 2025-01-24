using AuthServiceBulgakov.Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthServiceBulgakov.DataAccess.MSSQL.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.UserName).IsRequired();

            builder.ComplexProperty(x => x.Email, options =>
            {
                options.IsRequired();
                options.Property(o => o.Value).HasColumnName(nameof(User.Email));
            });

            builder.HasMany(x => x.Roles)
                   .WithMany(x => x.Users)
    .              UsingEntity<RoleUser>(
                        l => l.HasOne(x => x.Role).WithMany().HasForeignKey(x => x.RoleId),
                        r => r.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId),
                        k => k.HasKey(x => new { x.UserId, x.RoleId }));
        }
    }
}
