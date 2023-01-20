namespace CacheMetrics.Services;

public interface ICoffeeProvider
{
    Task<IEnumerable<CoffeeItem>> GetCoffeeItems();
}