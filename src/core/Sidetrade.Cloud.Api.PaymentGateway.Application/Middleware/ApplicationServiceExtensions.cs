using System.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Reflection;
using MediatR;
using Sidetrade.Cloud.Api.PaymentGateway.Application.VendorAccount.Commands.Create;

namespace Sidetrade.Cloud.Api.PaymentGateway.Application.Middleware;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IDbConnection>(connection => 
            new NpgsqlConnection(configuration.GetConnectionString("PaymentGatewayContext")));

        // services.Scan(selector =>
        // {
        //     selector
        //         .FromCallingAssembly()
        //         .AddClasses(filter => filter.AssignableTo(typeof(IRequestHandler<,>)))
        //         .AsImplementedInterfaces()
        //         .WithScopedLifetime();
        // });
        services.AddMediatR(typeof(CreateVendorAccountCommandHandler).GetTypeInfo().Assembly);
        return services;
    } 
}