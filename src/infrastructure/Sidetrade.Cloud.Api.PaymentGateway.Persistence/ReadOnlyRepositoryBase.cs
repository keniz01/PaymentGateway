using System.Linq.Expressions;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace Sidetrade.Cloud.Api.PaymentGateway.Persistence;

public abstract class ReadOnlyRepositoryBase<TModel>
    where TModel: class
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<ReadOnlyRepositoryBase<TModel>> _logger;

    public ReadOnlyRepositoryBase(ApplicationDbContext context, ILogger<ReadOnlyRepositoryBase<TModel>> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<TModel> GetAsync(Expression<Func<TModel, bool>> predicate, CancellationToken cancellationToken)
    {
        var response = await _context.Set<TModel>().FirstOrDefaultAsync(predicate, cancellationToken);
        return response!;
    }
}

public interface IRedisCacheRepository
{
    T Get<T>(string key);
    T Set<T>(string key, T value);
}

public class RedisCacheService : IRedisCacheRepository
{
    private readonly IDistributedCache _cache;

    public RedisCacheService(IDistributedCache cache)
    {
        _cache = cache;
    }

    public T Get<T>(string key)
    {
        var value = _cache.GetString(key);

        if (value != null)
        {
            return JsonSerializer.Deserialize<T>(value)!;
        }

        return default!;
    }

    public T Set<T>(string key, T value)
    {
        var timeOut = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(24),
            SlidingExpiration = TimeSpan.FromMinutes(60)
        };

        _cache.SetString(key, JsonSerializer.Serialize(value), timeOut);

        return value;
    }
}