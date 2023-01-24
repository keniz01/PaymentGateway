using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Hosting;
using Sidetrade.Cloud.Api.PaymentGateway.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using System.Diagnostics;
using Microsoft.Data.Sqlite;
using System.Data;
using Sidetrade.Cloud.Api.PaymentGateway.Api;

namespace Sidetrade.Cloud.Api.PaymentGateway.Tests.ApiTests;

public class TestWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        var connection = new SqliteConnection("Data Source=InMemoryTestDb;Mode=Memory;Cache=Shared");
        connection.Open();

        base.ConfigureWebHost(builder);
        builder.ConfigureServices(services =>
        {
            services.AddDataProtection();

            var contextDescriptor = services.SingleOrDefault(descriptor =>
                descriptor.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));
            services.Remove(contextDescriptor!);

            var connectionDescriptor = services.SingleOrDefault(descriptor =>
                descriptor.ServiceType == typeof(DbConnection));
            services.Remove(connectionDescriptor!);

            // https://learn.microsoft.com/en-us/dotnet/standard/data/sqlite/in-memory-databases
            // https://learn.microsoft.com/en-us/ef/core/testing/choosing-a-testing-strategy
            services.AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseSqlite(connection)
                        .EnableDetailedErrors()
                        .EnableSensitiveDataLogging()
                        .LogTo(message => Debug.WriteLine(message));
                });

            services.AddTransient<IDbConnection>(options => connection);
            var provider = services.BuildServiceProvider();
            using var serviceScope = provider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            using var context = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            var fakeData = BogusDataGenerator.Generate();

            foreach (var command in fakeData)
            {
                context.VendorAccounts.Add(command);
            }

            context.SaveChanges();
        });

        builder.UseEnvironment("Testing");
    }
}