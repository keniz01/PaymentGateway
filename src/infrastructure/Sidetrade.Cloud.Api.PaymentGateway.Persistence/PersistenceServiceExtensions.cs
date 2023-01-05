using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Sidetrade.Cloud.Api.PaymentGateway.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Sidetrade.Cloud.Api.PaymentGateway.Persistence;

public static class PersistenceServiceExtensions
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        //var connectionString = "server=localhost;port=5432;database=postgres;user id=postgres;password=Versuv10us";
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options
                .EnableDetailedErrors()
                .EnableSensitiveDataLogging()
                .LogTo(message => Debug.WriteLine(message))
                .UseNpgsql(configuration.GetConnectionString("PaymentGatewayContext"));
        });
        services.AddScoped<IVendorAccountReadOnlyRepository, VendorAccountReadOnlyRepository>();
        return services;
    }
}



