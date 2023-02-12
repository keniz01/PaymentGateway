using System.Data;
using Sidetrade.Cloud.Api.PaymentGateway.Application.Abstractions;
using Sidetrade.Cloud.Api.PaymentGateway.Application.Features.VendorAccountFeature.Queries;

namespace Sidetrade.Cloud.Api.PaymentGateway.Persistence;

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
