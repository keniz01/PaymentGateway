using Mapster;
using MapsterMapper;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PaymentGateway.Application;
using PaymentGateway.Application.Middleware;
using PaymentGateway.Consumers;
using PaymentGateway.Contracts;
using PaymentGateway.Persistence;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration(builder =>
    {
        builder.AddJsonFile($"{Path.GetDirectoryName(ConsumersAssembly.GetAssemblyReference().Location)}/appsettings.json");
        builder.AddEnvironmentVariables();
    })
    .ConfigureServices((context, services) =>
    {
        services.AddHostedService<ConsumerHostedService>();
        services.AddSingleton(new TypeAdapterConfig());
        services.AddScoped<IMapper, Mapper>();
        services.AddScoped<IVendorAccountCommandRepository, VendorAccountCommandRepository>();
        services.AddPersistenceServices(context.Configuration);
        services.AddApplicationServices(context.Configuration);
        services.Configure<RabbitMqConfig>(context.Configuration.GetSection(nameof(RabbitMqConfig)));

        services.AddMassTransit(options =>
        {
            options.AddConsumer<CreateVendorAccountConsumer>();
            options.SetKebabCaseEndpointNameFormatter();
            options.UsingRabbitMq((context, config) =>
            {
                var options = context.GetRequiredService<IOptions<RabbitMqConfig>>();
                var rabbitMqOptions = options.Value ?? new RabbitMqConfig();
                var rabbitMqConnectionString = $"amqp://{rabbitMqOptions.Username}:{rabbitMqOptions.Password}@{rabbitMqOptions.Host}:{rabbitMqOptions.Port}/";
                config.Host(rabbitMqConnectionString);
                config.ReceiveEndpoint(RabbitMqQueueNameConstants.CREATE_ACCOUNT_QUEUE, cfg =>
                {
                    cfg.ConfigureConsumer<CreateVendorAccountConsumer>(context);
                });
            });
        });
    })
    .ConfigureLogging((_, logging) => logging.AddConsole())
    .UseConsoleLifetime()
    .Build();

await host.RunAsync();
