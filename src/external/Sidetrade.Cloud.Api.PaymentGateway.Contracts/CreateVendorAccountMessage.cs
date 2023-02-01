namespace Sidetrade.Cloud.Api.PaymentGateway.Contracts;

public sealed class CreateVendorAccountMessage
{
    public int MemberId { get; set; }
    public int MetaMemberId { get; set; }
    public string ApiPublicKey { get; set; } = string.Empty;
    public string ApiSecretKey { get; set; } = string.Empty;
    public bool IsActivated { get; set; }
}

