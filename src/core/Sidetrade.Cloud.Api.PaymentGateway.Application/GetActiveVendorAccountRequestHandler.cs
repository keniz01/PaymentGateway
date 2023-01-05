using MediatR;
using Microsoft.Extensions.Logging;

namespace Sidetrade.Cloud.Api.PaymentGateway.Application;

public class GetActiveVendorAccountRequestHandler : IRequestHandler<GetActiveVendorAccountRequest, GetActiveVendorAccountResponse>
{
    private readonly ILogger<GetActiveVendorAccountRequestHandler> _logger;
    private readonly IVendorAccountReadOnlyRepository _vendorAccountReadOnlyRepository;

    public GetActiveVendorAccountRequestHandler(ILogger<GetActiveVendorAccountRequestHandler> logger,
        IVendorAccountReadOnlyRepository vendorAccountReadOnlyRepository)
    {
        _logger = logger;
        _vendorAccountReadOnlyRepository = vendorAccountReadOnlyRepository;
    }

    public async Task<GetActiveVendorAccountResponse> Handle(GetActiveVendorAccountRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("********* {LogData}: Request for {CorrelationId}", DateTime.UtcNow, request.CorrelationId);
        var response = await _vendorAccountReadOnlyRepository.GetActiveVendorAccountAsync(request, cancellationToken);
        return response;
    }
}
