namespace AuthServiceBulgakov.Api.Extensions
{
    public static class CorsExtension
    {
        public static IServiceCollection AddCustomCors(this IServiceCollection services)
        {
            services.AddCors(options => options.AddPolicy("AllowAllRequestFromlocalhost4200", builder =>
            {
                builder.WithOrigins("http://localhost:4200")
                       .AllowAnyHeader()
                       .AllowAnyMethod()
                       .AllowCredentials();
            }));

            return services;
        }
    }
}
