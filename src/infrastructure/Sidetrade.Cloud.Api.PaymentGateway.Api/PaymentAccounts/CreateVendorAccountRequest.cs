namespace Sidetrade.Cloud.Api.PaymentGateway.Api.PaymentAccounts;

public record CreateVendorAccountRequest(string SecretKey, string PublicKey, bool IsActivated);