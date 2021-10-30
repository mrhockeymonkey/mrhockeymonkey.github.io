# Microsoft Generic Host
:octicons-mark-github-16: Example Project: [GenericHost]("https://github.com/mrhockeymonkey/mrhockeymonkey.github.io/tree/master/docs/C%23/src/GenericHost")

## ASP.NET Generic Host
ASP.NET now uses the generic host by default
`WebHost` is still available for backwards support but should not be used

See: https://docs.microsoft.com/en-us/aspnet/core/fundamentals/host/generic-host?view=aspnetcore-5.0

```c#
public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}
```

## .NET Generic Host
Console apps can also use Generic Host to leverage the same benefits as webapps

See: https://docs.microsoft.com/en-us/dotnet/core/extensions/generic-host

```c#
public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                services.AddHostedService<Worker>();
            });
}
```