using FluentValidation;
using Mapster;
using MapsterMapper;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using PaymentGateway.Contracts;
using PaymentGateway.Presentation.Validators;

namespace PaymentGateway.Presentation;

public static class PresentationServiceExtensions
{
    public static IServiceCollection AddPresentationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton(new TypeAdapterConfig());
        services.AddScoped<IMapper, Mapper>();
        services.AddValidatorsFromAssemblyContaining(typeof(VendorIdValidator));
        services.Configure<RabbitMqConfig>(configuration.GetSection(nameof(RabbitMqConfig)));

        services.AddMassTransit(config =>
        {
            config.UsingRabbitMq((context, factory) =>
            {
                var options = context.GetRequiredService<IOptions<RabbitMqConfig>>();
                var rabbitMqOptions = options.Value ?? new RabbitMqConfig();
                var rabbitMqConnectionString = $"amqp://{rabbitMqOptions.Username}:{rabbitMqOptions.Password}@{rabbitMqOptions.Host}:{rabbitMqOptions.Port}/";
                factory.Host(rabbitMqConnectionString);
            });
        });
        return services;
    }
}

