using Microsoft.Extensions.Caching.Memory;
using Prometheus;

namespace CacheMetrics.BackgroundServices;

/// <summary>
/// Sets up a callback that will run before every metrics collections that will populate
/// the global IMemoryCache statistics
/// </summary>
public class PrometheusBeforeCollectionBootstrap : BackgroundService
{
    private readonly IMemoryCache _cache;
    
    private static readonly Gauge CurrentEntryCount = Metrics
        .CreateGauge("memory_cache_statistics_entry_count", "The number of ICacheEntry instances currently in the memory cache.");
    private static readonly Gauge TotalHits = Metrics
        .CreateGauge("memory_cache_statistics_hits_count", "The number of cache hits");
    private static readonly Gauge TotalMisses = Metrics
        .CreateGauge("memory_cache_statistics_misses_count", "The number of cache misses");
    private static readonly Gauge EstimatedSize = Metrics
        .CreateGauge("memory_cache_statistics_estimated_size", "An estimated sum of all cache entry sizes with no set unit");
    
    public PrometheusBeforeCollectionBootstrap(IMemoryCache cache)
    {
        _cache = cache ?? throw new ArgumentNullException(nameof(cache));
    }
    
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Metrics.DefaultRegistry.AddBeforeCollectCallback(() =>
        {
            var cacheStats = _cache.GetCurrentStatistics();
            
            if (cacheStats is null) return;
            
            CurrentEntryCount.Set(cacheStats.CurrentEntryCount);
            TotalHits.Set(cacheStats.TotalHits);
            TotalMisses.Set(cacheStats.TotalMisses);

            if (cacheStats.CurrentEstimatedSize is { } size)
            {
                EstimatedSize.Set(size);
            }
        });
        
        return Task.CompletedTask;
    }
}