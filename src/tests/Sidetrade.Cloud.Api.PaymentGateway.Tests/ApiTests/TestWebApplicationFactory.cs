using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Hosting;
using Sidetrade.Cloud.Api.PaymentGateway.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace Sidetrade.Cloud.Api.PaymentGateway.Tests;

public class TestWebApplicationFactory : WebApplicationFactory<Program>
    {
        private readonly string _dbName = Guid.NewGuid().ToString();

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
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
                
                services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase(_dbName));

                var app = services.BuildServiceProvider();
                using var context = app.GetService<ApplicationDbContext>();

                var fakeData = BogusDataGenerator.Generate();
                context!.ActiveVendorAccounts.AddRange(fakeData);
                context.SaveChanges();
            });

            builder.UseEnvironment("Development");
        }
    }