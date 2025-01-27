using AuthServiceBulgakov.DataAccess.MSSQL;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Collections.Concurrent;
using Testcontainers.MsSql;

namespace AuthServiceBulgakov.Tests
{
    public class CustomWebApplicationFactory
        : WebApplicationFactory<Program>, IAsyncLifetime
    {
        private readonly MsSqlContainer _sqlContainer = new MsSqlBuilder()
            .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
            .Build();

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services => 
            {
                services.RemoveAll(typeof(DbContextOptions<ApplicationDbContext>));

               
                services.AddDbContextFactory<ApplicationDbContext>((IServiceProvider sp, DbContextOptionsBuilder opts) =>
                    {
                        var test = _sqlContainer.GetConnectionString();

                        opts.UseSqlServer(_sqlContainer.GetConnectionString(),
                            (options) =>
                            {
                                options.EnableRetryOnFailure(
                                    maxRetryCount: 5,
                                    maxRetryDelay: TimeSpan.FromSeconds(30),
                                    errorNumbersToAdd: null);
                            });
                    });
            });         
        }

        public async Task InitializeAsync()
        {
            await _sqlContainer.StartAsync();
        }

        public new async Task DisposeAsync()
        {
            await _sqlContainer.StopAsync();
        }
    }
}
