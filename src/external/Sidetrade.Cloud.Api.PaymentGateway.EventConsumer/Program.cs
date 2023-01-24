using Mapster;
using MapsterMapper;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sidetrade.Cloud.Api.PaymentGateway.Application.Middleware;
using Sidetrade.Cloud.Api.PaymentGateway.EventConsumer;
using Sidetrade.Cloud.Api.PaymentGateway.EventConsumer.Consumers;
using Sidetrade.Cloud.Api.PaymentGateway.Persistence.Middleware;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration(builder =>
    {
        builder.AddJsonFile("appsettings.json");
        builder.AddEnvironmentVariables();
    })
    .ConfigureServices((context, services) =>
    {
        services.AddPersistenceServices(context.Configuration);
        services.AddApplicationServices(context.Configuration);
        services.AddSingleton(new TypeAdapterConfig());
        services.AddScoped<IMapper, ServiceMapper>();

        services.AddMassTransit(options =>
        {
            options.AddConsumer<CreateVendorAcccountEventConsumer>();
            options.SetKebabCaseEndpointNameFormatter();
            options.UsingRabbitMq((context, config) =>
            {
                config.Host("amqp://guest:guest@localhost:5672");
                config.ReceiveEndpoint("vendor-account", cfg => 
                {
                    cfg.ConfigureConsumer<CreateVendorAcccountEventConsumer>(context);
                });
            });
        });
    })
    .Build();

await host.RunAsync();

Console.WriteLine("Consumers listening now ....");
