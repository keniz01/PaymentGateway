namespace Sidetrade.Cloud.Api.PaymentGateway.Application;

/// <summary>
/// Request to get a vendor payment account by vendor id.
/// </summary>
public sealed class GetActiveVendorAccountResponse
{
    public int VendorId { get; set; }
    public int? MetaVendorId { get; set; }
    public string SecretKey { get; set; } = string.Empty;
    public string PublicKey { get; set; } = string.Empty;
    public bool IsActivated { get; set; }

    public override string ToString() => $"{VendorId} | {MetaVendorId} | {SecretKey} | {PublicKey} | {IsActivated}";
}