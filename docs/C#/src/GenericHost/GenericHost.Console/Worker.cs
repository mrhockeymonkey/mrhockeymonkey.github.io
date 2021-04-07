using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GenericHost.Console
{
    class Worker : IHostedService
    {
        private readonly ILogger<Worker> _logger;
        private readonly ISomeDependency _dep;
        private readonly WorkerOptions _options;

        public Worker(
            ILogger<Worker> logger, 
            ISomeDependency dep,
            IHostApplicationLifetime appLifetime,
            IOptions<WorkerOptions> options)
        {
            _logger = logger;
            _dep = dep;
            _options = options.Value;

            appLifetime.ApplicationStarted.Register(OnStarted);
            appLifetime.ApplicationStopping.Register(OnStopping);
            appLifetime.ApplicationStopped.Register(OnStopped);
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                _logger.LogInformation($"Got {_dep.GetSomeValue()}");
                await Task.Delay(TimeSpan.FromSeconds(_options.DelaySeconds));
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        private void OnStarted()
        {
            _logger.LogInformation("OnStarted has been called.");
        }

        private void OnStopping()
        {
            _logger.LogInformation("OnStopping has been called.");
        }

        private void OnStopped()
        {
            _logger.LogInformation("OnStopped has been called.");
        }
    }
}
