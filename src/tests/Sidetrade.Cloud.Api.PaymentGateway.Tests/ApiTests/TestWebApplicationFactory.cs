using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Sidetrade.Cloud.Api.PaymentGateway.Api.Helpers;
using Sidetrade.Cloud.Api.PaymentGateway.Application;
using Sidetrade.Cloud.Api.PaymentGateway.Application.Abstractions.Behaviours.Logging;
using Sidetrade.Cloud.Api.PaymentGateway.Application.Abstractions.Correlation;
using Sidetrade.Cloud.Api.PaymentGateway.Persistence;
using System.Data;
using System.Data.Common;
using System.Diagnostics;

namespace Sidetrade.Cloud.Api.PaymentGateway.Tests;

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

            services.AddScoped<ICorrelationIdHelper, CorrelationIdHelper>();
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(ApplicationAssembly.GetAssemblyReference()));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestLoggingBehavior<,>));

            // https://learn.microsoft.com/en-us/dotnet/standard/data/sqlite/in-memory-databases
            // https://learn.microsoft.com/en-us/ef/core/testing/choosing-a-testing-strategy
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlite(connection)
                    .EnableDetailedErrors()
                    .EnableSensitiveDataLogging()
                    .LogTo(message => Debug.WriteLine(message));
            });

            services.AddMassTransitTestHarness();
            services.AddTransient<IDbConnection>(options => connection);
            DataSeeder.Seed(services);
        });

        builder.UseEnvironment("Testing");
    }
}