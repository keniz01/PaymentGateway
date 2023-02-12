using Sidetrade.Cloud.Api.PaymentGateway.Application.Features.VendorAccountFeature.Queries;

namespace Sidetrade.Cloud.Api.PaymentGateway.Application.Abstractions;

public interface IVendorAccountQueryRepository
{
    Task<GetVendorAccountQueryResult> GetVendorAccountAsync(string sql, object parameters, CancellationToken cancellationToken);
}