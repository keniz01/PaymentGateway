namespace Sidetrade.Cloud.Api.PaymentGateway.Presentation.PaymentAccounts;

public record CreateVendorAccountRequest(string ApiSecretKey, string ApiPublicKey, bool IsActivated);