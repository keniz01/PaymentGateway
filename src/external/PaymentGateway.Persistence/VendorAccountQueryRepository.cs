using System.Data;
using PaymentGateway.Application.Abstractions;
using PaymentGateway.Application.Features.VendorAccountFeature.Queries;

namespace PaymentGateway.Persistence;

public class VendorAccountQueryRepository : QueryRepositoryBase, IVendorAccountQueryRepository
{
    public VendorAccountQueryRepository(IDbConnection connection): base(connection)
    {
    }

    public async Task<GetVendorAccountQueryResult> GetVendorAccountAsync(string sql, object parameters, CancellationToken cancellationToken)
    {
        var response = await GetAsync<GetVendorAccountQueryResult>(sql, parameters);
        return response;

    }
}
