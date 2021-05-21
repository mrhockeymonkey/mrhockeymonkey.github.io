using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace GenericHost.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            //CreateHostBuilder(args).Build().Run(); // or RunAsync()
            CreateCustomizedHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<ISomeDependency, SomeDependency>();
                    services.Configure<WorkerOptions>(hostContext.Configuration.GetSection(WorkerOptions.Worker));
                    services.AddHostedService<Worker>();
                })
                .UseConsoleLifetime(); //listens for CTRL+C or SIGTERM

        public static IHostBuilder CreateCustomizedHostBuilder(string[] args) =>
            new HostBuilder()
                .ConfigureHostConfiguration(host => {
                    host.SetBasePath(Directory.GetCurrentDirectory());
                    //host.AddJsonFile("hostsettings.json", optional: true);
                    //host.AddEnvironmentVariables(prefix: "PREFIX_");
                    //host.AddCommandLine(args);
                })
                .ConfigureAppConfiguration(app => {
                    app.AddJsonFile("appsettings.json", optional: false, reloadOnChange: false);
                })
                .ConfigureServices((hostContext, services) => 
                {
                    services.AddSingleton<ISomeDependency, SomeDependency>();
                    services.Configure<WorkerOptions>(hostContext.Configuration.GetSection(WorkerOptions.Worker));
                    services.AddHostedService<Worker>();
                })
                .ConfigureLogging(logging =>
                {
                    logging.AddConsole();
                })
                .UseConsoleLifetime();
    }
}
