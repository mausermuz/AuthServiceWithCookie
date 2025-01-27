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

        private ConcurrentDictionary<string, HttpClient> HttpClients { get; } =
            new ConcurrentDictionary<string, HttpClient>();

        public void AddHttpClient(string clientName, HttpClient client)
        {
            if (!HttpClients.TryAdd(clientName, client))
            {
                throw new InvalidOperationException(
                    $"HttpClient with name {clientName} is already added");
            }
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services => 
            {
                services.RemoveAll(typeof(DbContextOptions<ApplicationDbContext>));

                services.AddSingleton<IHttpClientFactory>(new CustomHttpClientFactory(HttpClients));
                
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
