using Microsoft.Extensions.Logging;
using Sidetrade.Cloud.Api.PaymentGateway.Application;

namespace Sidetrade.Cloud.Api.PaymentGateway.Persistence;

public class VendorAccountReadOnlyRepository : ReadOnlyRepositoryBase<GetActiveVendorAccountResponse>, IVendorAccountReadOnlyRepository
{
    private readonly ILogger<ReadOnlyRepositoryBase<GetActiveVendorAccountResponse>> _logger;
    public VendorAccountReadOnlyRepository(ApplicationDbContext context, ILogger<ReadOnlyRepositoryBase<GetActiveVendorAccountResponse>> logger)
        : base(context, logger)
    {
        _logger = logger;
    }

    public async Task<GetActiveVendorAccountResponse> GetActiveVendorAccountAsync(GetActiveVendorAccountRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("********* {LogData}: Request for {CorrelationId}", DateTime.UtcNow, request.CorrelationId);
        var response = await GetAsync(account => account.VendorId == request.VendorId && account.IsActivated, cancellationToken);
        return response ??= new();
    }
}