

namespace Progress;

/// <summary>
/// Provides an IProgress<T> that counts total and completed items and
/// periodically raises an event containing the percent progressed as a double
///
/// Does not capture SynchronizationContext like Progress<T>
/// </summary>
public sealed class PeriodicPercentProgress<T> : IProgress<T>, IAsyncDisposable
{
    private int _total;
    private int _updated;
    private bool _shouldNotify;
    private Task _delayTask;
    private int _delayMs = 500;
    private bool _disposed;
    private static readonly SemaphoreSlim ShouldNotifySemaphore = new(1,1);

    public event Action<double>? ProgressChanged;
    
    public PeriodicPercentProgress(int delayMs, int total = 0)
    {
        _delayMs = delayMs;
        _total = total;
        _updated = 0;
        _shouldNotify = true;
        _delayTask = Task.CompletedTask;
    }
    
    public void Report(T value)
    {
        Interlocked.Increment(ref _updated);
        _ = DelayedNotification();
    }

    /// <summary>
    /// Allows you to report the total sometime after you have already started reporting progress
    /// For example if you start getting item details before you have got all pages of items from a rest api
    /// </summary>
    public void ReportTotal(int total) => _total = total;
    
    private async Task DelayedNotification()
    {
        if (!_shouldNotify) return;

        await ShouldNotifySemaphore.WaitAsync();
        try
        {
            if (!_shouldNotify) return;
            _shouldNotify = false;
            
            _delayTask = Task.Delay(_delayMs);
            await _delayTask;
            
            ProgressChanged?.Invoke(Progress);

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
        _disposed = true;
    }
    
}
