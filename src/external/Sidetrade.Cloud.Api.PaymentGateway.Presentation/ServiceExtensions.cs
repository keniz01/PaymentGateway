using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using MassTransit;
using Sidetrade.Cloud.Api.PaymentGateway.Presentation.Validators;

namespace Sidetrade.Cloud.Api.PaymentGateway.Presentation;

public static class ServiceExtensions
{
    public static IServiceCollection AddApiMappings(this IServiceCollection services)
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

