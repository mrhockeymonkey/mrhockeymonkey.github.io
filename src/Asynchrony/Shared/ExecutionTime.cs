using System.Diagnostics;

namespace Shared;

public class ExecutionTime
{
    public static async Task<TimeSpan> MeasureAsync(Func<Task> action)
    {
        Stopwatch sw = new();
        sw.Start();
        await action();
        sw.Stop();
        return sw.Elapsed;
    }
    
    public static TimeSpan Measure(Action action)
    {
        Stopwatch sw = new();
        sw.Start();
        action();
        sw.Stop();
        return sw.Elapsed;
    }
}