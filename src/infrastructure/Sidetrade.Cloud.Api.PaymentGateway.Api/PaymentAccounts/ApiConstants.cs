namespace Sidetrade.Cloud.Api.PaymentGateway.Api.PaymentAccounts;

public abstract class HttpRequestHeaderNameConstants
{
    public const string CORRELATION_ID = "X-CORRELATION-ID";
    public const string VENDOR_ID = "X-MEMBER-ID";
}

public abstract class ApiEndpointConstants
{
    public const string GET_VENDOR_ACCOUNT = "api/v1/payment-gateway/account";
}