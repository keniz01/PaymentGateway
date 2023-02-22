using Mapster;
using MapsterMapper;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sidetrade.Cloud.Api.PaymentGateway.Application;
using Sidetrade.Cloud.Api.PaymentGateway.Application.Middleware;
using Sidetrade.Cloud.Api.PaymentGateway.Consumers;
using Sidetrade.Cloud.Api.PaymentGateway.Persistence;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration(builder =>
    {
        builder.AddJsonFile("appsettings.json");
        builder.AddEnvironmentVariables();
    })
    .ConfigureServices((context, services) =>
    {
        services.AddSingleton(new TypeAdapterConfig());
        services.AddScoped<IMapper, Mapper>();
        services.AddScoped<IVendorAccountCommandRepository, VendorAccountCommandRepository>();
        services.AddPersistenceServices(context.Configuration);
        services.AddApplicationServices(context.Configuration);

        services.AddMassTransit(options =>
        {
            options.AddConsumer<CreateVendorAccountConsumer>();
            options.SetKebabCaseEndpointNameFormatter();
            options.UsingRabbitMq((context, config) =>
            {
                config.Host("amqp://guest:guest@localhost:5672");
                config.ReceiveEndpoint("vendor-account", cfg => 
                {
                    cfg.ConfigureConsumer<CreateVendorAccountConsumer>(context);
                });
            });
        });
    })
    .Build();

await host.RunAsync();

Console.WriteLine("Hello, World!");
