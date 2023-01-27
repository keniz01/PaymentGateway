using System.Diagnostics;
using Sidetrade.Cloud.Api.PaymentGateway.Application.Abstractions.Correlation;
using Sidetrade.Cloud.Api.PaymentGateway.Presentation.Features.VendorAccountFeature;

namespace Sidetrade.Cloud.Api.PaymentGateway.Api.Configurations;

public class LogRequestContextMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<LogRequestContextMiddleware> _logger;

    public LogRequestContextMiddleware(
        RequestDelegate next,
        ILogger<LogRequestContextMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public Task InvokeAsync(HttpContext context, ICorrelationIdHelper correlationIdHelper)
    {
        var correlationId = GetCorrelationId(context, correlationIdHelper);
        AddCorrelationIdHeaderToResponse(context, correlationId);

        var timer = new Stopwatch();
        timer.Start();
        _logger.LogInformation("********* Request Id: {CorrelationId} started at {LogData}. *********", correlationId.ToString(), DateTimeOffset.UtcNow);

        var response = _next(context);

        _logger.LogInformation("********* Request Id: {CorrelationId} completed after {TimeElapsed} ms. *********", correlationId.ToString(), timer.ElapsedMilliseconds);
        return response;
    }

    private static Guid GetCorrelationId(HttpContext context, ICorrelationIdHelper correlationIdHelper)
    {
        Guid correlationId;
        var correlationIdHeaderValue = context.Request.Headers
            .SingleOrDefault(header => header.Key.ToLowerInvariant().Equals(HttpRequestHeaderNameConstants.CORRELATION_ID.ToLowerInvariant()))
            .Value
            .FirstOrDefault();

        if (string.IsNullOrWhiteSpace(correlationIdHeaderValue)
            || !Guid.TryParse(correlationIdHeaderValue, out correlationId)
            || correlationId == Guid.Empty)
        {
            correlationId = Guid.NewGuid();
        }

        correlationIdHelper.Set(correlationId);
        return correlationId;
    }

    private static void AddCorrelationIdHeaderToResponse(HttpContext context, Guid correlationId)
        => context.Response.OnStarting(() =>
            {
                context
                    .Response
                    .Headers
                    .Add(HttpRequestHeaderNameConstants.CORRELATION_ID, correlationId.ToString());
                return Task.CompletedTask;
            });
}