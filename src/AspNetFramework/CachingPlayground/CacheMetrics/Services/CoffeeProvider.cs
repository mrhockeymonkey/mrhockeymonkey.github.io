namespace CacheMetrics.Services;

public class CoffeeProvider : ICoffeeProvider
{
    private readonly ILogger<CoffeeProvider> _logger;

    public CoffeeProvider(ILogger<CoffeeProvider> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    public async Task<IEnumerable<CoffeeItem>> GetCoffeeItems()
    {
        _logger.LogWarning("Making expensive call to a slow db!");
        await Task.Delay(2000);
        return new CoffeeItem[]
        {
            new("Flat White", 3.6m),
            new("Long Black", 3.0m),
            new("Batch Brew", 2.8m),
            new("Espresso", 2.8m),
            new("Americano", 3.1m)
        };
    }
}