using Microsoft.AspNetCore.SignalR;

namespace Progress;

public sealed class ReconcilerProgressReporter : IReconcilerProgressReporter
{
    private readonly IHubContext<ActivityHub, IActivity> _activityHub;
    private readonly ILogger<ReconcilerProgressReporter> _logger;
    private int _total;
    private int _updated;
    private bool _shouldNotify;
    private Task _delayTask;
    private const int DelayTimeMs = 500;
    private bool _disposed;
    private static readonly SemaphoreSlim ShouldNotifySemaphore = new(1,1);
    
    public ReconcilerProgressReporter(
        IHubContext<ActivityHub, IActivity> activityHub, 
        ILogger<ReconcilerProgressReporter> logger)
    {
        _activityHub = activityHub ?? throw new ArgumentNullException(nameof(activityHub));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _total = 0;
        _updated = 0;
        _shouldNotify = true;
        _delayTask = Task.CompletedTask;

    }

    public void ReportTotal(int total) => _total = total;

    public void ReportAssetUpdated()
    {
        Interlocked.Increment(ref _updated);
        _ = DelayedNotification();
    }


    private async Task DelayedNotification()
    {
        if (!_shouldNotify) return;

        await ShouldNotifySemaphore.WaitAsync();
        try
        {
            if (!_shouldNotify) return;
            _shouldNotify = false;
            
            _delayTask = Task.Delay(DelayTimeMs);
            await _delayTask;
            
            _logger.LogInformation($"Reconcile progress: {Progress:P1}");
            await _activityHub.Clients.All.SendReconcileProgress(Progress);
            _shouldNotify = true;
        }
        finally
        {
            ShouldNotifySemaphore.Release();
        }
    }
    
    private double Progress => _total <= 0 ? 0d : (double)_updated / _total;

    public async ValueTask DisposeAsync()
    {
        if (_disposed) return;
        
        await _delayTask;
        await _activityHub.Clients.All.SendReconcileComplete();
        _logger.LogInformation("Reconcile progress reporting complete");
        _disposed = true;
    }
}
