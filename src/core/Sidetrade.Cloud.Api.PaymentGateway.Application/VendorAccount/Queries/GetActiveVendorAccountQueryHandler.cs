using Microsoft.Extensions.Logging;
using Sidetrade.Cloud.Api.PaymentGateway.Application.Abstractions;
using Sidetrade.Cloud.Api.PaymentGateway.Application.Shared;

namespace Sidetrade.Cloud.Api.PaymentGateway.Application.VendorAccount;

public class GetActiveVendorAccountRequestHandler : QueryHandlerBase<GetActiveVendorAccountQuery, GetActiveVendorAccountQueryResult>
{    
    private readonly IVendorAccountQueryRepository _repository;

    public GetActiveVendorAccountRequestHandler(
        ILogger<GetActiveVendorAccountRequestHandler> logger,
        IVendorAccountQueryRepository repository) : base(logger)
    {    
        _repository = repository;
    }

    public override async Task<GetActiveVendorAccountQueryResult> Handle(GetActiveVendorAccountQuery request, CancellationToken cancellationToken)
    {
        var sqlQuery = "SELECT * FROM vendor_account WHERE member_id = @MemberId";
        var parameters = new { request.MemberId };
        
        var result = await _repository.GetAsync<GetActiveVendorAccountQueryResult>(sqlQuery, parameters, cancellationToken);
        return result ?? GetActiveVendorAccountQueryResult.Unknown(request.CorrelationId);
    }
}