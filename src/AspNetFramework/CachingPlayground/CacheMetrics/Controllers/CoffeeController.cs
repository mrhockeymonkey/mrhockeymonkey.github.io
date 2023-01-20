using CacheMetrics.Services;
using Microsoft.AspNetCore.Mvc;

namespace CacheMetrics.Controllers;

[ApiController]
[Route("[controller]")]
public class CoffeeController : ControllerBase
{
    private readonly ICoffeeProvider _coffeeProvider;

    public CoffeeController(ICoffeeProvider coffeeProvider)
    {
        _coffeeProvider = coffeeProvider ?? throw new ArgumentNullException(nameof(coffeeProvider));
    }

    [HttpGet]
    public async Task<IEnumerable<CoffeeItem>> Get()
    {
        return await _coffeeProvider.GetCoffeeItems();
    }
}