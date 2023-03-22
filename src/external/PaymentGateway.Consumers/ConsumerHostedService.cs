using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace PaymentGateway.Consumers
{
    public sealed class ConsumerHostedService : IHostedService
    {
        private readonly ILogger _logger;

        public ConsumerHostedService(ILogger<ConsumerHostedService> logger, IHostApplicationLifetime appLifetime)
        {
            _logger = logger;
            appLifetime.ApplicationStarted.Register(OnStarted);
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("<=============== Starting subscribers =============>");
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("<=============== Subscribers stopped =============>");
            return Task.CompletedTask;
        }

        private void OnStarted() => _logger.LogInformation("<=============== Subscribers now listening =============>");
    }
}