namespace Sidetrade.Cloud.Api.PaymentGateway.Application;

/// <summary>
/// Request to get a vendor payment account by vendor id.
/// </summary>
public sealed class GetActiveVendorAccountRequest: RequestBase<GetActiveVendorAccountResponse>
{
    /// <param name="correlationId">Correlation identifier.</param>
    /// <param name="vendorId">Vendor identifier.</param>
    public GetActiveVendorAccountRequest(int vendorId, Guid correlationId) : base(correlationId) => VendorId = vendorId;
    public int VendorId { get; init; }
}
