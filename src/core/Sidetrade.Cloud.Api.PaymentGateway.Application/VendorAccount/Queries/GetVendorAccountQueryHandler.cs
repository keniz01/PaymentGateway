using Sidetrade.Cloud.Api.PaymentGateway.Application.Abstractions;
using Sidetrade.Cloud.Api.PaymentGateway.Application.Abstractions.Queries;

namespace Sidetrade.Cloud.Api.PaymentGateway.Application.VendorAccount;

public class GetVendorAccountQueryHandler : IQueryHandler<GetVendorAccountQuery, GetVendorAccountQueryResult>
{    
    private readonly IVendorAccountQueryRepository _repository;

    public GetVendorAccountQueryHandler(
        IVendorAccountQueryRepository repository)
    {    
        _repository = repository;
    }

    public async Task<GetVendorAccountQueryResult> Handle(GetVendorAccountQuery request, CancellationToken cancellationToken)
    {
        var sql = "SELECT * FROM vendor_account WHERE member_id = @MemberId";
        var parameters = new { request.MemberId };
        
        var result = await _repository.GetAsync<GetVendorAccountQueryResult>(sql, parameters, cancellationToken);
        return result ?? GetVendorAccountQueryResult.Unknown();
    }
}