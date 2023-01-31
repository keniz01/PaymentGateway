using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Sidetrade.Cloud.Api.PaymentGateway.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Mapster;
using MapsterMapper;
using Dapper;
using Sidetrade.Cloud.Api.PaymentGateway.Application.Abstractions;

namespace Sidetrade.Cloud.Api.PaymentGateway.Persistence;

public static class PersistenceServiceExtensions
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        DefaultTypeMap.MatchNamesWithUnderscores = true;
        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            options.EnableDetailedErrors();
            options.EnableSensitiveDataLogging();
            options.LogTo(message => Debug.WriteLine(message));
            options.UseNpgsql(configuration.GetConnectionString("PaymentGatewayContext"));
        });

        services.AddScoped<IVendorAccountCommandRepository, VendorAccountCommandRepository>();
        services.AddScoped<IVendorAccountQueryRepository, VendorAccountQueryRepository>();
        services.AddSingleton(new TypeAdapterConfig());
        services.AddScoped<IMapper, ServiceMapper>();
        return services;
    }
}