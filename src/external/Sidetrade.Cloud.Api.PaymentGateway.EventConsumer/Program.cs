using Mapster;
using MapsterMapper;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Sidetrade.Cloud.Api.PaymentGateway.EventConsumer.Consumers;
using Microsoft.Extensions.DependencyInjection;
using Sidetrade.Cloud.Api.PaymentGateway.Application;
using Sidetrade.Cloud.Api.PaymentGateway.Persistence;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration(builder =>
    {
        builder.AddJsonFile("appsettings.json");
        builder.AddEnvironmentVariables();
    })
    .ConfigureServices((_, services) =>
    {
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

Console.WriteLine("Hello, World!");
