using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sidetrade.Cloud.Api.PaymentGateway.Application.Shared;

namespace Sidetrade.Cloud.Api.PaymentGateway.Persistence;

public abstract class QueryRepositoryBase<TResult>
    where TResult: ResultBase
{
    private readonly ApplicationDbContext _context;

    public QueryRepositoryBase(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<TResult> GetAsync(Expression<Func<TResult, bool>> predicate, CancellationToken cancellationToken)
    {
        var response = await _context.Set<TResult>().AsNoTracking().FirstOrDefaultAsync(predicate, cancellationToken);
        return response!;
    }
}