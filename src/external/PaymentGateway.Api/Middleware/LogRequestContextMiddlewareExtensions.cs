namespace PaymentGateway.Api.Middleware;

public static class LogRequestContextMiddlewareExtensions
{
    public static IApplicationBuilder UseRequestContextLogger(this IApplicationBuilder app)
        => app.UseMiddleware<LogRequestContextMiddleware>();
}