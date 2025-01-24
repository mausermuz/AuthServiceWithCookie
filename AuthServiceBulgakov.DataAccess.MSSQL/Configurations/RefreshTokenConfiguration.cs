using AuthServiceBulgakov.Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthServiceBulgakov.DataAccess.MSSQL.Configurations
{
    public class RefreshTokenConfiguration : BaseConfiguration<RefreshToken>
    {
        public override void Config(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.ToTable("RefreshTokens");

            builder.Property(x => x.Token).HasMaxLength(200).IsRequired();
            builder.HasIndex(x => x.Token).IsUnique();

            builder.HasOne(x => x.User)
                   .WithOne(x => x.RefreshToken);
        }
    }
}
