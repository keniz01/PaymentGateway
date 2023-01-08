using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Sidetrade.Cloud.Api.PaymentGateway.Application.Shared
{
    public abstract class CommandHandlerBase<TCommand> : IRequestHandler<TCommand, Unit>
        where TCommand : CommandBase
    {
        private readonly ILogger<CommandHandlerBase<TCommand>> _logger;

        public CommandHandlerBase(ILogger<CommandHandlerBase<TCommand>> logger)
        {
            _logger = logger;
        }

        public async Task<Unit> HandleAsync(TCommand command, CancellationToken cancellationToken)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            _logger.LogDebug("********* {LogData}: Request for command: {CorrelationId}", DateTime.UtcNow, command.CorrelationId);

            try
            {
                await Handle(command, cancellationToken);
                _logger.LogDebug("********* {LogData}: Response for command: {CorrelationId}", DateTime.UtcNow, command.CorrelationId);
                return Unit.Value;
            }
            finally
            {
                stopWatch.Stop();
                _logger.LogDebug("Response for command: {ElapsedMilliseconds} served (elapsed time: {1} msec)", typeof(TCommand).Name, stopWatch.ElapsedMilliseconds);
            }
        }

        public abstract Task<Unit> Handle(TCommand command, CancellationToken cancellationToken);
    }
}

//string dbConnectionString = this.Configuration.GetConnectionString("dbConnection1");

//// Inject IDbConnection, with implementation from SqlConnection class.
//services.AddTransient<IDbConnection>((sp) => new SqlConnection(dbConnectionString));

