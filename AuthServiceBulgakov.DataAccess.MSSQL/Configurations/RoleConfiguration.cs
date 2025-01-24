using AuthServiceBulgakov.Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthServiceBulgakov.DataAccess.MSSQL.Configurations
{
    public class RoleConfiguration : BaseConfiguration<Role>
    {
        public override void Config(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Roles");

            builder.Property(x => x.Name).IsRequired();
        }
    }
}
