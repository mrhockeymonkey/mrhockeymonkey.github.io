namespace CS.Result;

public static class Result
{
    public static IResult<T, Exception> Ok<T>(T value) => new Ok<T>(value);
    // public static IResult<T, TError> Ok<T, TError>(T value) => new Ok<T, TError>(value);
    public static IResult<T, Exception> Error<T>(Exception error) => new Error<T, Exception>(error);
    public static IResult<T, TError> Error<T, TError>(TError error) where TError : Exception
        => new Error<T, TError>(error);
}