namespace Sidetrade.Cloud.Api.PaymentGateway.Presentation.PaymentAccounts;

public record CreateVendorAccountRequest(string SecretKey, string PublicKey, bool IsActivated);