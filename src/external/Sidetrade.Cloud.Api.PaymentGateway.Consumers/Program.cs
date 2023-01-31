using Mapster;
using MapsterMapper;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Sidetrade.Cloud.Api.PaymentGateway.Consumers;

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
            options.AddConsumer<CreateVendorAcccountConsumer>();
            options.SetKebabCaseEndpointNameFormatter();
            options.UsingRabbitMq((context, config) =>
            {
                config.Host("amqp://guest:guest@localhost:5672");
                config.ReceiveEndpoint("vendor-account", cfg => 
                {
                    cfg.ConfigureConsumer<CreateVendorAcccountConsumer>(context);
                });
            });
        });
    })
    .Build();

await host.RunAsync();

Console.WriteLine("Hello, World!");
