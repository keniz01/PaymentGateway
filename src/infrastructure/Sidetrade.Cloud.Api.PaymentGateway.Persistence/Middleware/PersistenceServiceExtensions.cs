using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Sidetrade.Cloud.Api.PaymentGateway.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Mapster;
using MapsterMapper;

namespace Sidetrade.Cloud.Api.PaymentGateway.Persistence;

public static class PersistenceServiceExtensions
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options => 
            options
                .EnableDetailedErrors()
                    .EnableSensitiveDataLogging()
                        .LogTo(message => Debug.WriteLine(message))
                            .UseNpgsql(configuration.GetConnectionString("PaymentGatewayContext")));

        services.AddScoped<IVendorAccountCommandRepository, VendorAccountCommandRepository>();
        services.AddSingleton(new TypeAdapterConfig());
        services.AddScoped<IMapper, ServiceMapper>();
        return services;
    }
}



