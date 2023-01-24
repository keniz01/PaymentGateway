namespace Sidetrade.Cloud.Api.PaymentGateway.Application.Abstractions.Behaviours.Logging;

using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Sidetrade.Cloud.Api.PaymentGateway.Application.Abstractions.Correlation;

public class RequestLoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> 
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<RequestLoggingBehavior<TRequest, TResponse>> _logger;
    private readonly ICorrelationIdHelper _correlationIdHelper;
    public RequestLoggingBehavior(
        ILogger<RequestLoggingBehavior<TRequest, TResponse>> logger,
        ICorrelationIdHelper correlationIdHelper)
    {
        _logger = logger;
        _correlationIdHelper = correlationIdHelper;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var correlationId = _correlationIdHelper.Get();
        var timer = new Stopwatch();
        timer.Start();
        _logger.LogInformation("********* Request Id: {CorrelationId} started at {LogData}. *********", correlationId.ToString(), DateTimeOffset.UtcNow);
        
        var response = await next();
        
        _logger.LogInformation("********* Request Id: {CorrelationId} completed after {TimeElapsed} ms. *********", correlationId.ToString(), timer.ElapsedMilliseconds);
        return response;
    }
}