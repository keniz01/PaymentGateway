using System.Data.Common;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using Sidetrade.Cloud.Api.PaymentGateway.Application.Abstractions.Correlation;

public class DatabaseContextLogInterceptor : DbCommandInterceptor
{
    private readonly ILogger<DatabaseContextLogInterceptor> _logger;
    private readonly ICorrelationIdHelper _correlationIdHelper;

    public DatabaseContextLogInterceptor(ILogger<DatabaseContextLogInterceptor> logger, ICorrelationIdHelper correlationIdHelper)
    {
        _logger = logger;
        _correlationIdHelper = correlationIdHelper;
    }

    public override ValueTask<DbDataReader> ReaderExecutedAsync(DbCommand command, CommandExecutedEventData eventData, DbDataReader result, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("********* Request Id: {CorrelationId} completed after {TimeElapsed} ms. *********", _correlationIdHelper.Get(), eventData.Duration.TotalMilliseconds);
        return base.ReaderExecutedAsync(command, eventData, result, cancellationToken);
    }
}