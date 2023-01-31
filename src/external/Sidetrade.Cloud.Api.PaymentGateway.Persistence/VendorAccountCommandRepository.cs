using Sidetrade.Cloud.Api.PaymentGateway.Application;
using MapsterMapper;
using Sidetrade.Cloud.Api.PaymentGateway.Domain.Entities;

namespace Sidetrade.Cloud.Api.PaymentGateway.Persistence;

public class VendorAccountCommandRepository : RepositoryBase<DbVendorAccount>, IVendorAccountCommandRepository
{  
    private readonly IMapper _mapper;
    public VendorAccountCommandRepository(ApplicationDbContext context,
        IMapper mapper) : base(context) => _mapper = mapper;

    public async Task<bool> CreateVendorAccountAsync(VendorAccountEntity entity, CancellationToken cancellationToken)
    {
        var model = _mapper.Map<DbVendorAccount>(entity);
        AddModel(model);
        return await SaveChangesAsync(cancellationToken); 
    }

    public async Task<bool> UpdateVendorAccountAsync(VendorAccountEntity entity, CancellationToken cancellationToken)
    {
        var model = _mapper.Map<DbVendorAccount>(entity);
        UpdateModel(model);
        return await SaveChangesAsync(cancellationToken); 
    }

    public async Task<bool> DeleteVendorAccountAsync(VendorAccountEntity entity, CancellationToken cancellationToken)
    {
        var model = _mapper.Map<DbVendorAccount>(entity);
        DeleteModel(model);
        return await SaveChangesAsync(cancellationToken); 
    }
}
