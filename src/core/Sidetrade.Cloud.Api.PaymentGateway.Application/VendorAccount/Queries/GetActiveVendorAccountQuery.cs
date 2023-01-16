using Sidetrade.Cloud.Api.PaymentGateway.Application.Shared;

namespace Sidetrade.Cloud.Api.PaymentGateway.Application.VendorAccount;

/// <summary>
/// Request to get a vendor payment account by member id.
/// </summary>
public sealed class GetActiveVendorAccountQuery: QueryBase<GetActiveVendorAccountQueryResult>
{
    /// <param name="correlationId">Correlation identifier.</param>
    /// <param name="memberId">Vendor identifier.</param>
    public GetActiveVendorAccountQuery(int memberId, Guid correlationId) : base(correlationId) => MemberId = memberId;
    public int MemberId { get; init; }
}
