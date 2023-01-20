using Microsoft.Extensions.Caching.Memory;

namespace CacheMetrics.Services;

public class CoffeeProviderCache : ICoffeeProvider
{
    private readonly ICoffeeProvider _provider;
    private readonly IMemoryCache<CoffeeProviderCache> _cache;
    private readonly string _key = nameof(CoffeeProviderCache);

    public CoffeeProviderCache(ICoffeeProvider provider, IMemoryCache<CoffeeProviderCache> cache)
    {
        _provider = provider ?? throw new ArgumentNullException(nameof(provider));
        _cache = cache ?? throw new ArgumentNullException(nameof(cache));
    }
    
    public async Task<IEnumerable<CoffeeItem>> GetCoffeeItems()
    {
        var cached = await _cache.GetOrCreateAsync(_key, entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(10);
            return _provider.GetCoffeeItems();
        });

        return cached ?? new List<CoffeeItem>(); // TODO do I like this?
    }
}