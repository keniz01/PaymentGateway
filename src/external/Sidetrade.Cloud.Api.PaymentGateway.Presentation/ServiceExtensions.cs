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
        var config = new TypeAdapterConfig();
        // Or
        // var config = TypeAdapterConfig.GlobalSettings;
        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>();
        services.AddValidatorsFromAssemblyContaining(typeof(VendorIdValidator));
        return services;
    }
}

