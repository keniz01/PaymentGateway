// See https://aka.ms/new-console-template for more information
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration(cfg =>
    {
        cfg.AddJsonFile("appsettings.json");
    })
    .ConfigureServices(services => 
    {
        services.AddMassTransit(x =>
        {
            x.AddConsumer<SubmitOrderConsumer>(typeof(SubmitOrderConsumerDefinition));

            x.SetKebabCaseEndpointNameFormatter();

            x.UsingRabbitMq((context, cfg) => cfg.ConfigureEndpoints(context));
        });
    })
    .Build();

await host.RunAsync();

Console.WriteLine("Hello, World!");
