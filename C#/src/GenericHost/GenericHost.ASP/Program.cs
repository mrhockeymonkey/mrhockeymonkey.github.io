using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace GenericHost.ASP
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //CreateHostBuilder(args).Build().Run();
            CreateCustomizedHostBuilder(args).Build().Run();
            //CreateWebHostBuilderFromScratch(args).Build.Run();
        }

        // this is the standard way to define a webhost
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            // configures host with content root and DOTNET_ env vars
            // configures app with appsettings.ENV.json, env vars, command line, logging, etc
            Host.CreateDefaultBuilder(args)
                // use kestral, IIS integration, webrootfiler provider, middleware
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    // startup then further configures services
                    webBuilder.UseStartup<Startup>();
                });

        // You can customize and override defaults as per your requirements. 
        public static IHostBuilder CreateCustomizedHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                //.UseEnvironment("Development")
                //.UseContentRoot("/Some/Other/Dir")
                .ConfigureHostConfiguration(host =>
                {
                    // you can configure some host setting here
                    // likely you want to config WEB host settings in ConfigureWebHost below
                })
                .ConfigureAppConfiguration(configureApp =>
                {
                    // prefixed environment variables
                    configureApp.AddEnvironmentVariables(prefix: "MYAPP_");
                    // include settings from an xml, ini or from memory
                    configureApp.AddIniFile("appsettings.ini");
                    configureApp.AddXmlFile("appsettings.xml", optional: true, reloadOnChange: false);
                    configureApp.AddInMemoryCollection(new Dictionary<string, string>() {
                        { "MyMemoryKey", "MyMemoryValue" }
                    });

                    // add existing or externally built config, maybe if you need to apply validation to it
                    configureApp.AddConfiguration(new ConfigurationBuilder().AddJsonFile("foo.json").Build());
                })
                .ConfigureLogging(logging =>
                {
                    logging.SetMinimumLevel(LogLevel.Warning);
                })
                .ConfigureWebHost(webHost =>
                {
                    webHost.UseKestrel();
                    webHost.UseIIS();
                    webHost.UseIISIntegration();
                    webHost.ConfigureKestrel((context, options) =>
                    {
                        options.Limits.MaxRequestBodySize = 20000000;
                    });
                    //webHost.UseEnvironment("Development");
                    //webHost.UseUrls("http://*:5000");
                    webHost.UseStartup<Startup>();

                    // alternitively to UseStartup you can do the below
                    //webHost.Configure(app => {});
                    //webHost.ConfigureServices(services => {});
                });

        // this is an example of building the host from scratch, i.e. no defaults whatsoever.
        public static IHostBuilder CreateWebHostBuilderFromScratch(string[] args) =>
            new HostBuilder()
                .ConfigureHostConfiguration(host => { })
                .ConfigureAppConfiguration(app => { })
                .ConfigureLogging(logging => { })
                .ConfigureWebHost(webHost => {
                    webHost.UseKestrel();
                    webHost.UseStartup<Startup>();
                });
    }
}
