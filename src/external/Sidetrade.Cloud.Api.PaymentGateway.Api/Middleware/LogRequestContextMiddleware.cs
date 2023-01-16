namespace Sidetrade.Cloud.Api.PaymentGateway.Api.PaymentAccounts;

public class LogRequestContextMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<LogRequestContextMiddleware> _logger;

    public LogRequestContextMiddleware(RequestDelegate next, ILogger<LogRequestContextMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public Task InvokeAsync(HttpContext context)
    {
        var correlationId = Guid.Empty;
        var correlationIdHeaderValue = context.Request.Headers
            .SingleOrDefault(header => header.Key.ToLowerInvariant().Equals(HttpRequestHeaderNameConstants.CORRELATION_ID.ToLowerInvariant()))
            .Value
            .FirstOrDefault();

        if(string.IsNullOrWhiteSpace(correlationIdHeaderValue) 
            || !Guid.TryParse(correlationIdHeaderValue, out correlationId) 
            || correlationId == Guid.Empty)
        {
            correlationId = Guid.NewGuid();
            context
                .Response
                .Headers
                .Add(HttpRequestHeaderNameConstants.CORRELATION_ID, correlationId.ToString());
        }

        _logger.LogInformation("********* {LogData}: Request for {CorrelationId}", DateTime.UtcNow, correlationId.ToString());
        return _next(context);
    }
}