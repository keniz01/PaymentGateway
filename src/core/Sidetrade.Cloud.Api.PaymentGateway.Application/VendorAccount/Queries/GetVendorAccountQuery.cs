using Sidetrade.Cloud.Api.PaymentGateway.Application.Abstractions.Queries;

namespace Sidetrade.Cloud.Api.PaymentGateway.Application.VendorAccount;

/// <summary>
/// Request to get a vendor payment account by member id.
/// </summary>
public record GetVendorAccountQuery(int MemberId): IQuery<GetVendorAccountQueryResult>;