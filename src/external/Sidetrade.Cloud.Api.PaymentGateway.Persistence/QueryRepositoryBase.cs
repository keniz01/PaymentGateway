using System.Data;
using Dapper;

namespace Sidetrade.Cloud.Api.PaymentGateway.Persistence;

public abstract class QueryRepositoryBase
{
    private readonly IDbConnection _connection;

    protected QueryRepositoryBase(IDbConnection connection)
    {
        _connection = connection;
    }

    public async Task<T> GetAsync<T>(string sql, object parameters)
    {
        var result = await _connection.QueryFirstOrDefaultAsync<T>
        (
            sql,
            parameters
        );

        return result;
    }
}
