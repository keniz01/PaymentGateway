using PaymentGateway.Application.Abstractions.Queries;

namespace PaymentGateway.Application.Features.VendorAccountFeature.Queries;

/// <summary>
/// Request to get a vendor payment account by member id.
/// </summary>
public record GetVendorAccountQuery(int MemberId) : IQuery<GetVendorAccountQueryResult>;