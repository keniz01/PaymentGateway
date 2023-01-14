using System.Data;
using Dapper;
using Microsoft.Extensions.Logging;
using Sidetrade.Cloud.Api.PaymentGateway.Application.Shared;

namespace Sidetrade.Cloud.Api.PaymentGateway.Application.VendorAccount;

public class GetActiveVendorAccountRequestHandler : QueryHandlerBase<GetActiveVendorAccountQuery, GetActiveVendorAccountQueryResult>
{
    private readonly IDbConnection _connection;

    public GetActiveVendorAccountRequestHandler(
        ILogger<GetActiveVendorAccountRequestHandler> logger,
        IDbConnection connection)
        : base(logger)
    {
        _connection = connection;
    }

    public override async Task<GetActiveVendorAccountQueryResult> Handle(GetActiveVendorAccountQuery request, CancellationToken cancellationToken)
    {
        var sqlQuery = "SELECT * FROM vendor_account WHERE vendor_id = @vendor_id";
        var parameters = new DynamicParameters();
        parameters.Add("@vendor_id", request.VendorId, DbType.Int32);
        var result = await _connection.QueryFirstOrDefaultAsync<GetActiveVendorAccountQueryResult>(sqlQuery, parameters);
        return result;
    }
}