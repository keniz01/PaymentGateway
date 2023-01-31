using Microsoft.EntityFrameworkCore;

namespace Sidetrade.Cloud.Api.PaymentGateway.Persistence;

public abstract class RepositoryBase<T> where T : class 
{
    private readonly ApplicationDbContext _context;
    
    public RepositoryBase(ApplicationDbContext context) => _context = context;

    public void AddModel(T model) => _context.Entry(model).State = EntityState.Added;

    public void UpdateModel(T model) => _context.Entry(model).State = EntityState.Modified;

    public void DeleteModel(T model) => _context.Entry(model).State = EntityState.Deleted;

    public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken)
    {
        var affectedRows = await _context.SaveChangesAsync(cancellationToken);
        return affectedRows == 1;
    }
}
