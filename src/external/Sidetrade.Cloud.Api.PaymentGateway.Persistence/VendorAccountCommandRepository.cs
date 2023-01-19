using Microsoft.Extensions.Logging;
using Sidetrade.Cloud.Api.PaymentGateway.Application;
using Sidetrade.Cloud.Api.PaymentGateway.Application.VendorAccount.Commands.Create;
using MapsterMapper;

namespace Sidetrade.Cloud.Api.PaymentGateway.Persistence;

public class VendorAccountCommandRepository : IVendorAccountCommandRepository
{
    private readonly ILogger<VendorAccountCommandRepository> _logger;
    private readonly IMapper _mapper;
    private readonly ApplicationDbContext _context;
    public VendorAccountCommandRepository(
        ApplicationDbContext context, 
        ILogger<VendorAccountCommandRepository> logger,
        IMapper mapper)
    {
        _logger = logger;
        _context = context;
        _mapper = mapper;
    }

    public async Task<bool> CreateVendorAccountAsync(CreateVendorAccountCommand command, CancellationToken cancellationToken)
    {
        // _logger.LogInformation("********* {LogDate}: Request for {CorrelationId}", DateTime.UtcNow);
        
        var model = _mapper.Map<VendorAccount>(command);

        _context.VendorAccounts.Add(model);
        var affectedRows = await _context.SaveChangesAsync(cancellationToken);
        
        // _logger.LogInformation("********* {LogDate}: Response for {CorrelationId}", DateTime.UtcNow, command.CorrelationId);
        return affectedRows == 1;
    }
}
