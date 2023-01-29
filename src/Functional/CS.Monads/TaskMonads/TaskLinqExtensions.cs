namespace CS.Monads.TaskMonads;

public static class TaskLinqExtensions
{
    // i.e. the "Many" here refers to Tasks instead of IEnumerable if you compare to 
    // https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.selectmany?view=net-7.0

    public static async Task<TResult> SelectMany<TSource, TOther, TResult>(
        this Task<TSource> source, Func<TSource, Task<TOther>> taskSelector, 
        Func<TSource, TOther, TResult> resultSelector)
    {
        var t = await source;
        var u = await taskSelector(t);
        return resultSelector(t, u);
    }

    // notice this IS the Bind operation above
    public static async Task<TResult> SelectMany<TSource, TResult>(this Task<TSource> source, Func<TSource, Task<TResult>> selector) => 
        await selector(await source);
    
    public static async Task<TResult> Select<T, TResult>(this Task<T> task, Func<T, TResult> project) =>
        project(await task.ConfigureAwait(false));
}