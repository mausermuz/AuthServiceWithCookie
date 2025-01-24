using AuthServiceBulgakov.Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AuthServiceBulgakov.DataAccess.MSSQL
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddMsSQL(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DbConnection");

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            return services;
        }
    }
}
