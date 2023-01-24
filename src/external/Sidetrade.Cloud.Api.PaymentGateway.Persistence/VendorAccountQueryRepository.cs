using System.Data;
using Dapper;
using Sidetrade.Cloud.Api.PaymentGateway.Application.Abstractions.Repositories;

namespace Sidetrade.Cloud.Api.PaymentGateway.Persistence;

public class VendorAccountQueryRepository : IVendorAccountQueryRepository
{
    private readonly IDbConnection _connection;

    public VendorAccountQueryRepository(IDbConnection connection)
    {
        _connection = connection;
    }

    public async Task<T> GetAsync<T>(string sql, object parameters, CancellationToken cancellationToken) where T : class
    {
        var result = await _connection.QueryFirstOrDefaultAsync<T>
        (
            sql, 
            parameters
        );

        return result;
    }
}
