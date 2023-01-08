using Microsoft.Extensions.Logging;
using Sidetrade.Cloud.Api.PaymentGateway.Application.Shared;

namespace Sidetrade.Cloud.Api.PaymentGateway.Application;

public class GetActiveVendorAccountRequestHandler : QueryHandlerBase<GetActiveVendorAccountQuery, GetActiveVendorAccountQueryResult>
{
    private readonly IVendorAccountQueryRepository _vendorAccountReadRepository;

    public GetActiveVendorAccountRequestHandler(ILogger<GetActiveVendorAccountRequestHandler> logger,
        IVendorAccountQueryRepository vendorAccountReadRepository)
        : base(logger)
    {
        _vendorAccountReadRepository = vendorAccountReadRepository;
    }

    public override async Task<GetActiveVendorAccountQueryResult> Handle(GetActiveVendorAccountQuery request, CancellationToken cancellationToken)
    {
        var response = await _vendorAccountReadRepository.GetActiveVendorAccountAsync(request, cancellationToken);
        return response;
    }
}
