using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace PaymentGateway.Persistence;

public abstract class CommandRepositoryBase<T> where T : class, new()
{
    private readonly ApplicationDbContext _context;

    protected CommandRepositoryBase(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> AnyModelAsync(Expression<Func<T, bool>> predicate) => await _context.Set<T>().AnyAsync(predicate);

    public async Task<T> GetModelAsync(Expression<Func<T, bool>> predicate) => await _context.Set<T>().FirstAsync(predicate);

    public void AddModel(T model) => _context.Entry(model).State = EntityState.Added;

    public void UpdateModel(T model) => _context.Entry(model).State = EntityState.Modified;

    public void DeleteModel(T model) => _context.Entry(model).State = EntityState.Deleted;

    public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken)
    {
        var affectedRows = await _context.SaveChangesAsync(cancellationToken);
        return affectedRows == 1;
    }
}
