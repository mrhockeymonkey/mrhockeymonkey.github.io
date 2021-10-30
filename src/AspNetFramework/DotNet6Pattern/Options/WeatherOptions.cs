using System.Collections.Generic;

namespace DotNet6Pattern.Options;
public class WeatherOptions
{
    public const string Weather = "Weather";
    public string[]? Summaries { get; set; }
    public string Thing { get; set; } = "MyOtherThing";
}