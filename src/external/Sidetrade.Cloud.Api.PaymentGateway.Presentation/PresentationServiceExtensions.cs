using FluentValidation;
using Mapster;
using MapsterMapper;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Sidetrade.Cloud.Api.PaymentGateway.Presentation.Features.VendorAccountFeature;

namespace Sidetrade.Cloud.Api.PaymentGateway.Presentation;

public static class PresentationServiceExtensions
{
    public static IServiceCollection AddPresentationServices(this IServiceCollection services)
    {
        services.AddSingleton(new TypeAdapterConfig());
        services.AddScoped<IMapper, ServiceMapper>();
        services.AddValidatorsFromAssemblyContaining(typeof(VendorIdValidator));
        services.AddMassTransit(config =>
        {
            config.UsingRabbitMq((_, factory) =>
            {
                factory.Host("amqp://guest:guest@localhost:5672");
            });
        });

        return services;
    }
}

