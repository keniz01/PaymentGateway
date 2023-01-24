namespace Sidetrade.Cloud.Api.PaymentGateway.Presentation.Features.VendorAccountFeature;

public record CreateVendorAccountRequest(string ApiSecretKey, string ApiPublicKey, bool IsActivated);