using System.Collections.Concurrent;

namespace CS.Memoize;

public static class MemoizeExtensions
{
    public static Func<TInput, TResult> Memoize<TInput, TResult>(this Func<TInput, TResult> func)
    where TInput : IComparable
    {
        Dictionary<TInput, TResult> cache = new();
        return arg =>
        {
            if (cache.ContainsKey(arg)) return cache[arg];
            return cache[arg] = func(arg);
        };
    }

    public static Func<TInput, TResult> MemoizeThreadSafe<TInput, TResult>(this Func<TInput, TResult> func)
    where TInput : IComparable
    {
        ConcurrentDictionary<TInput, TResult> cache = new();
        return arg => cache.GetOrAdd(arg, func); 
        // no guarantee that simultaneous access wont result in duplicate evaluation
        // https://learn.microsoft.com/en-us/dotnet/api/system.collections.concurrent.concurrentdictionary-2.getoradd?view=net-7.0
    }

    public static Func<TInput, TResult> MemoizeThreadSafeLazy<TInput, TResult>(this Func<TInput, TResult> func)
        where TInput : IComparable
    {
        ConcurrentDictionary<TInput, Lazy<TResult>> cache = new();
        return arg => cache.GetOrAdd(arg, new Lazy<TResult>(() => func(arg))).Value; 
    }
}