using AuthServiceBulgakov.Application.Services;
using AuthServiceBulgakov.Infrastructure.Impl.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AuthServiceBulgakov.Infrastructure.Impl
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IJwtTokenService, JwtTokenService>();
            services.AddScoped<IRefreshTokenService, RefreshTokenService>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();

            return services;
        }
    }
}
