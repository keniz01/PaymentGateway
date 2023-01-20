// See https://aka.ms/new-console-template for more information
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Sidetrade.Cloud.Api.PaymentGateway.EventConsumer.Consumers;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration(cfg =>
    {
        cfg.AddJsonFile("appsettings.json");
    })
    .ConfigureServices(services => 
    {
        services.AddMassTransit(x =>
        {
            x.AddConsumer<CreateVendorAcccountEventConsumer>(typeof(CreateVendorAcccountEventConsumer));

            x.SetKebabCaseEndpointNameFormatter();

            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.ConfigureEndpoints(context);
                cfg.Host("localhost", "/", h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });
            });
        });
    })
    .Build();

await host.RunAsync();

Console.WriteLine("Hello, World!");
