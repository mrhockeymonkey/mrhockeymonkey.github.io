using Microsoft.Extensions.Caching.Memory;
using Prometheus;

namespace CacheMetrics;

/// <summary>
/// Decorator for <c>IMemoryCache&lt;T&gt;</c> to increment prometheus counters with hit/miss counts per type T
/// </summary>
/// <typeparam name="T"></typeparam>
public sealed class InstrumentedMemoryCache<T> : IMemoryCache<T>
{
    private readonly IMemoryCache _cache;
    private readonly string _typeName;
    
    private readonly Counter _totalHits = Metrics
        .CreateCounter("typed_memory_cache_hits_count", "The number of cache hits", "type_name");
    private readonly Counter _totalMisses = Metrics
        .CreateCounter("typed_memory_cache_misses_count", "The number of cache misses", "type_name");

    public InstrumentedMemoryCache(IMemoryCache cache)
    {
        _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        _typeName = typeof(T).Name;
    }

    public void Dispose() => _cache.Dispose();

    public ICacheEntry CreateEntry(object key) => _cache.CreateEntry(key);

    public void Remove(object key) => _cache.Remove(key);

    public bool TryGetValue(object key, out object? value)
    {
        if (_cache.TryGetValue(key, out value))
        {
            _totalHits.WithLabels(_typeName).Inc();
            return true;
        }
        
        _totalMisses.WithLabels(_typeName).Inc();
        return false;
    }
}