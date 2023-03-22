using MapsterMapper;
using PaymentGateway.Application;
using PaymentGateway.Domain.Entities;

namespace PaymentGateway.Persistence;

public class VendorAccountCommandRepository : CommandRepositoryBase<DbVendorAccount>, IVendorAccountCommandRepository
{  
    private readonly IMapper _mapper;
    public VendorAccountCommandRepository(ApplicationDbContext context,
        IMapper mapper) : base(context) => _mapper = mapper;

    public async Task<bool> CreateVendorAccountAsync(VendorAccountEntity entity, CancellationToken cancellationToken)
    {
        var hasModel = await AnyModelAsync(model => model.MemberId == entity.MemberId);
        if (hasModel)
        {
            return false;
        } 

        var model = _mapper.Map<DbVendorAccount>(entity);
        model.DateCreated = model.DateUpdated = DateTimeOffset.UtcNow;
        AddModel(model);
        return await SaveChangesAsync(cancellationToken); 
    }

    public async Task<bool> UpdateVendorAccountAsync(VendorAccountEntity entity, CancellationToken cancellationToken)
    {
        var currentModel = await GetModelAsync(model => model.MemberId == entity.MemberId);
        if (currentModel is null)
        {
            return false;
        }

        var model = _mapper.Map<DbVendorAccount>(entity);

        ApplyChanges(currentModel, model);
        currentModel.DateUpdated = DateTimeOffset.UtcNow;
        UpdateModel(model);
        return await SaveChangesAsync(cancellationToken); 
    }

    public async Task<bool> DeleteVendorAccountAsync(VendorAccountEntity entity, CancellationToken cancellationToken)
    {
        var model = _mapper.Map<DbVendorAccount>(entity);
        DeleteModel(model);
        return await SaveChangesAsync(cancellationToken); 
    }

    private static void ApplyChanges<T>(T currentModel, T newModel)
    {
        foreach (var currentProperty in currentModel!.GetType().GetProperties())
        {
            var property = newModel?.GetType().GetProperty(currentProperty.Name);

            if (currentProperty.Name == property?.Name)
            {
                currentProperty.SetValue(currentModel, property.GetValue(newModel, null));
            }
        }
    }
}
