using System.Runtime.InteropServices.JavaScript;

namespace CS.Result;

public static class ResultExtensions
{
    public static IResult<T, Exception> ToResult<T>(this T value) => Result.Ok(value);
    public static IResult<T, Exception> ToResult<T>(this Exception error) => Result.Error<T>(error);

    public static IResult<TResult, Exception> Map<T, TResult>(
        this IResult<T, Exception> result, 
        Func<T, TResult> func) 
    {
        // switch (result)
        // {
        //     case Ok<T> ok:
        //     {
        //         try/catch ???
        //     }
        // } 
        return result switch
        {
            Ok<T> ok => Result.Ok(func(ok.Value)), // TODO what if exception thrown?
            Error<T, Exception> error => Result.Error<TResult>(error.Exception),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    public static IResult<TResult, Exception> Bind<T, TResult>(
        this IResult<T, Exception> result,
        Func<T, IResult<TResult, Exception>> func)
    {
        return result switch
        {
            Ok<T> ok => func(ok.Value),
            Error<T, Exception> error => Result.Error<TResult>(error.Exception),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}