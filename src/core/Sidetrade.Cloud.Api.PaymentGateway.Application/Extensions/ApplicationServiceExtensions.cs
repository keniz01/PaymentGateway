using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sidetrade.Cloud.Api.PaymentGateway.Application.Abstractions.Behaviours.Logging;
using Sidetrade.Cloud.Api.PaymentGateway.Application.Abstractions.Correlation;
using Sidetrade.Cloud.Api.PaymentGateway.Application.Features.VendorAccountFeature.Commands.Create;
using System.Reflection;

namespace Sidetrade.Cloud.Api.PaymentGateway.Application.Middleware;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ICorrelationIdHelper, CorrelationIdHelper>();
        services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
        //services.Scan(scan =>
        //    scan.FromAssemblyOf<CreateVendorAccountCommandHandler>()
        //        .AddClasses(classes => classes.AssignableTo(typeof(IRequestHandler<,>)))
        //            .AsImplementedInterfaces()
        //                .WithScopedLifetime());
        //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestLoggingBehavior<,>));
        return services;
    } 
}