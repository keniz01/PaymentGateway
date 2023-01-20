using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using Sidetrade.Cloud.Api.PaymentGateway.Presentation.PaymentAccounts;
using FluentValidation;

namespace Sidetrade.Cloud.Api.PaymentGateway.Presentation;

public static class ServiceExtensions
{
    public static IServiceCollection AddApiMappings(this IServiceCollection services)
    {
        services.AddSingleton(new TypeAdapterConfig());
        services.AddScoped<IMapper, ServiceMapper>();
        services.AddValidatorsFromAssemblyContaining(typeof(VendorIdValidator));
        return services;
    }
}

