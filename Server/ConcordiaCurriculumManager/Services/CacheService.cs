using Microsoft.Extensions.Caching.Memory;

namespace ConcordiaCurriculumManager.Services;

public interface ICacheService<T>
{
    bool Exists(string key);
    T? GetOrCreate(string key, Func<(T cacheEntry, TimeSpan expiryDate, bool neverRemove)> createItem);
}

public class CacheService<T> : ICacheService<T>
{
    private readonly IMemoryCache _cache;
    private readonly ILogger<CacheService<T>> _logger;
    private readonly ReaderWriterLockSlim _lock;

    public CacheService(IMemoryCache cache, ILogger<CacheService<T>> logger)
    {
        _cache = cache;
        _logger = logger;
        _lock = new ReaderWriterLockSlim();
    }

    public T? GetOrCreate(string key, Func<(T cacheEntry, TimeSpan expiryDate, bool neverRemove)> createItem)
    {
        try
        {
            _lock.EnterReadLock();
            T? cacheEntry = _cache.Get<T>(key);
            if (cacheEntry is not null) 
                return cacheEntry;
        }
        finally
        {
            _lock.ExitReadLock();
        }

        try
        {
            _lock.EnterWriteLock();
            var (cacheEntry, expiryDate, neverRemove) = createItem();
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                                        .SetAbsoluteExpiration(expiryDate)
                                        .SetPriority(neverRemove ? CacheItemPriority.NeverRemove : CacheItemPriority.Normal);

            _logger.LogInformation($"Adding a cache entry till {expiryDate}");
            return _cache.Set(key, cacheEntry, cacheEntryOptions);
        }
        finally
        {
            _lock.ExitWriteLock();
        }
    }

    public bool Exists(string key)
    {
        try
        {
            _lock.EnterReadLock();
            return _cache.TryGetValue(key, out _);
        }
        finally
        {
            _lock.ExitReadLock();
        }
    }
}
