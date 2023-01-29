namespace CS.Monads.TaskMonads;

public static class TaskMonadExtensions
{
    
    public static Task<T> ToTask<T>(this T value) => Task.FromResult(value);

    public static async Task<TResult> Bind<T, TResult>(this Task<T> task, Func<T, Task<TResult>> continuation) =>
        await continuation(await task.ConfigureAwait(false))
            .ConfigureAwait(false);
    
    public static async Task<TResult> Map<T, TResult>(this Task<T> task, Func<T, TResult> func) =>
        func(await task.ConfigureAwait(false));
    

    /// <summary>
    /// Useful to use in a pipeline to cause a side effect with a given value but return the original value 
    /// </summary>
    public static async Task<TResult> Tap<TResult>(this Task<TResult> task, Func<TResult, Task> action)
    {
        await action(await task);
        return await task;
    }
    

}