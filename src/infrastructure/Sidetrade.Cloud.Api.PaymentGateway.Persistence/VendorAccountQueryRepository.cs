using Microsoft.Extensions.Logging;
using Sidetrade.Cloud.Api.PaymentGateway.Application;

namespace Sidetrade.Cloud.Api.PaymentGateway.Persistence;

public class VendorAccountQueryRepository : QueryRepositoryBase<GetActiveVendorAccountQueryResult>, IVendorAccountQueryRepository
{
    private readonly ILogger<VendorAccountQueryRepository> _logger;
    public VendorAccountQueryRepository(ApplicationDbContext context, ILogger<VendorAccountQueryRepository> logger)
        : base(context)
    {
        _logger = logger;
    }

    public async Task<GetActiveVendorAccountQueryResult> GetActiveVendorAccountAsync(GetActiveVendorAccountQuery request, CancellationToken cancellationToken)
    {
         _logger.LogInformation("********* {LogData}: Request for {CorrelationId}", DateTime.UtcNow, request.CorrelationId);
        var response = await GetAsync(account => account.VendorId == request.VendorId && account.IsActivated, cancellationToken);
        response.SetCorrelationId(request.CorrelationId);
        _logger.LogInformation("********* {LogData}: Response for {CorrelationId}", DateTime.UtcNow, response.CorrelationId);
        return response ??= GetActiveVendorAccountQueryResult.Unknown(request.CorrelationId);
    }
}