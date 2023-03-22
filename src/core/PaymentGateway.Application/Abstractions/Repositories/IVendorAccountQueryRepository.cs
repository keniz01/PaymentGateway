using PaymentGateway.Application.Features.VendorAccountFeature.Queries;

namespace PaymentGateway.Application.Abstractions;

public interface IVendorAccountQueryRepository
{
    Task<GetVendorAccountQueryResult> GetVendorAccountAsync(string sql, object parameters, CancellationToken cancellationToken);
}