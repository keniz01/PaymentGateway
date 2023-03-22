using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PaymentGateway.Application.Abstractions.Behaviours.Logging;
using PaymentGateway.Application.Abstractions.Correlation;
using PaymentGateway.Application.Features.VendorAccountFeature.Commands.Create;
using System.Reflection;

namespace PaymentGateway.Application.Middleware;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ICorrelationIdHelper, CorrelationIdHelper>();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(ApplicationAssembly.GetAssemblyReference()));
        return services;
    } 
}