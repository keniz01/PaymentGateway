using Sidetrade.Cloud.Api.PaymentGateway.Application.Shared;

namespace Sidetrade.Cloud.Api.PaymentGateway.Application;

/// <summary>
/// Request to get a vendor payment account by vendor id.
/// </summary>
public sealed class GetActiveVendorAccountQuery: QueryBase<GetActiveVendorAccountQueryResult>
{
    /// <param name="correlationId">Correlation identifier.</param>
    /// <param name="vendorId">Vendor identifier.</param>
    public GetActiveVendorAccountQuery(int vendorId, Guid correlationId) : base(correlationId) => VendorId = vendorId;
    public int VendorId { get; init; }
}
