using System.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Reflection;
using MediatR;
using Sidetrade.Cloud.Api.PaymentGateway.Application.VendorAccount.Commands.Create;
using Sidetrade.Cloud.Api.PaymentGateway.Application.Abstractions.Correlation;
using Sidetrade.Cloud.Api.PaymentGateway.Application.Abstractions.Behaviours.Logging;

namespace Sidetrade.Cloud.Api.PaymentGateway.Application.Middleware;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IDbConnection>(connection => 
            new NpgsqlConnection(configuration.GetConnectionString("PaymentGatewayContext")));

        services.AddScoped<ICorrelationIdHelper, CorrelationIdHelper>();
        services.AddMediatR(typeof(CreateVendorAccountCommandHandler).GetTypeInfo().Assembly);
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestLoggingBehavior<,>));
        return services;
    } 
}