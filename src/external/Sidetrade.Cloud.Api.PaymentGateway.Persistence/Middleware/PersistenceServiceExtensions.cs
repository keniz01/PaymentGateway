using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Mapster;
using MapsterMapper;
using Dapper;
using Microsoft.Extensions.Logging;
using Sidetrade.Cloud.Api.PaymentGateway.Application.Abstractions.Correlation;
using Sidetrade.Cloud.Api.PaymentGateway.Application.Abstractions.Repositories;

namespace Sidetrade.Cloud.Api.PaymentGateway.Persistence.Middleware;

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

            var logger = sp.GetRequiredService<ILogger<DatabaseContextLogInterceptor>>();
            var correlationIdHelper = sp.GetRequiredService<ICorrelationIdHelper>();
            options.AddInterceptors(new DatabaseContextLogInterceptor(logger, correlationIdHelper));
        });

        services.AddScoped<IVendorAccountCommandRepository, VendorAccountCommandRepository>();
        services.AddScoped<IVendorAccountQueryRepository, VendorAccountQueryRepository>();
        services.AddSingleton(new TypeAdapterConfig());
        services.AddScoped<IMapper, ServiceMapper>();
        return services;
    }
}