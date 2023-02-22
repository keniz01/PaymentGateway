using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Sidetrade.Cloud.Api.PaymentGateway.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Mapster;
using MapsterMapper;
using Dapper;
using Sidetrade.Cloud.Api.PaymentGateway.Application.Abstractions;
using Npgsql;
using System.Data;

namespace Sidetrade.Cloud.Api.PaymentGateway.Persistence;

public static class PersistenceServiceExtensions
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("PaymentGatewayContext");
        DefaultTypeMap.MatchNamesWithUnderscores = true;

        services.AddTransient<IDbConnection>(connection => new NpgsqlConnection(connectionString));

        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            options.EnableDetailedErrors();
            options.EnableSensitiveDataLogging();
            options.LogTo(message => Debug.WriteLine(message));
            options.UseNpgsql(connectionString);
        });

        services.AddScoped<IVendorAccountCommandRepository, VendorAccountCommandRepository>();
        services.AddScoped<IVendorAccountQueryRepository, VendorAccountQueryRepository>();
        services.AddSingleton(new TypeAdapterConfig());
        services.AddScoped<IMapper, ServiceMapper>();
        return services;
    }
}