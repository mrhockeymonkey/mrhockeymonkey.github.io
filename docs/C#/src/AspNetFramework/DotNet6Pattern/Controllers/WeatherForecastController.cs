using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using DotNet6Pattern.Options;

namespace DotNet6Pattern.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger;
    private readonly WeatherOptions _options;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IOptions<WeatherOptions> options)
    {
        _logger = logger;
        _options = options.Value;
    }

    [HttpGet]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            // Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            Summary = _options.Summaries[Random.Shared.Next(_options.Summaries.Length)]
        })
        .ToArray();
    }
}
