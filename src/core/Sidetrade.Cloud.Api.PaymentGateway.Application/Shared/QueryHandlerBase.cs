using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace Sidetrade.Cloud.Api.PaymentGateway.Application.Shared
{
    public abstract class QueryHandlerBase<TQuery, TResult> : IQueryHandler<TQuery, TResult>
        where TQuery : QueryBase<TResult>
        where TResult : ResultBase
    {
        private readonly ILogger<QueryHandlerBase<TQuery, TResult>> _logger;

        protected QueryHandlerBase(ILogger<QueryHandlerBase<TQuery, TResult>> logger)
        {
            _logger = logger;
        }

        public async Task<TResult> HandleAsync(TQuery query, CancellationToken cancellationToken)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            _logger.LogDebug("********* {LogData}: Request for query: {CorrelationId}", DateTime.UtcNow, query.CorrelationId);

            try
            {
                var result = await Handle(query, cancellationToken);
                _logger.LogDebug("********* {LogData}: Response for query: {CorrelationId}", DateTime.UtcNow, result.CorrelationId);
                return result;
            }
            finally
            {
                stopWatch.Stop();
                _logger.LogDebug("Response for query: {ElapsedMilliseconds} served (elapsed time: {1} msec)", typeof(TQuery).Name, stopWatch.ElapsedMilliseconds);
            }
        }

        public abstract Task<TResult> Handle(TQuery request, CancellationToken cancellationToken);
    }
}

