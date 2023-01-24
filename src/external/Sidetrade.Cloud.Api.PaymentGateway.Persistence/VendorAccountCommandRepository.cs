using Microsoft.Extensions.Logging;
using MapsterMapper;
using Sidetrade.Cloud.Api.PaymentGateway.Domain.Entities;
using Sidetrade.Cloud.Api.PaymentGateway.Application.Abstractions.Repositories;

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

    public async Task<bool> CreateVendorAccountAsync(VendorAccountEntity entity, CancellationToken cancellationToken)
    {
        var model = _mapper.Map<VendorAccountDataModel>(entity);

        _context.VendorAccounts.Add(model);
        var affectedRows = await _context.SaveChangesAsync(cancellationToken);
        
        return affectedRows == 1;
    }
}
