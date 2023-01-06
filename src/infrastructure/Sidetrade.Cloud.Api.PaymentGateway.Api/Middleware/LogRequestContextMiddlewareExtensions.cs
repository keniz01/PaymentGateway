namespace Sidetrade.Cloud.Api.PaymentGateway.Api.PaymentAccounts;

public static class LogRequestContextMiddlewareExtensions
{
    public static IApplicationBuilder UseRequestContextLogger(this IApplicationBuilder app)
    {
        app.UseMiddleware<LogRequestContextMiddleware>();
        return app;
    }
}