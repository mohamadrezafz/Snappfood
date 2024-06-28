
using Microsoft.Extensions.Caching.Memory;
using Snappfood.Application.Interfaces.Caching;

namespace Snappfood.Infrastructure.Services.Caching;

public class MemoryCacheProvider : ICacheProvider
{
    private const int CacheSeconds = 10;

    private readonly IMemoryCache _cache;

    public MemoryCacheProvider(IMemoryCache cache)
    {
        _cache = cache;
    }


    public void ClearCache(string key)
    {
        _cache.Remove(key);
    }

    public T? GetFromCache<T>(string key) where T : class
    {
        var cachedResponse = _cache.Get(key);
        return cachedResponse as T;
    }

    public void SetCache<T>(string key, T value) where T : class
    {
        SetCache(key, value, DateTimeOffset.Now.AddSeconds(CacheSeconds));
    }

    public void SetCache<T>(string key, T value, DateTimeOffset duration) where T : class
    {
        _cache.Set(key, value, duration);
    }
}
