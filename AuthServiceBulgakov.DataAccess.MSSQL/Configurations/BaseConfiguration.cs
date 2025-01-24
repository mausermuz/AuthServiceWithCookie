using AuthServiceBulgakov.Domain.Seedwork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthServiceBulgakov.DataAccess.MSSQL.Configurations
{
    /// <summary>
    /// Базовый класс конфигурции для всех сущностей
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class BaseConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
        where TEntity : BaseEntity
    {
        public abstract void Config(EntityTypeBuilder<TEntity> builder);
        public void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(x => x.Id);
            Config(builder);
        }
    }
}
