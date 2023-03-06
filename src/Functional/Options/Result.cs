namespace Options;

public struct Result<T, TErr>
{
    private T? _value;
    private TErr? _error;

    public static Result<T, TErr> Ok(T value) => new(value);
    public static Result<T, TErr> Error(TErr error) => new(error);


    public Result(T value)
    {
        _value = value;
    }

    public Result(TErr error)
    {
        _error = error;
    }
}


public interface IResult<T, out TErr>
{
    bool HasValue { get; }
    T ValueOrDefault(T otherwise);
    T ValueOrDefault(Func<T> otherwise);
    T? ValueOrDefault();
    TErr? GetError();
}

public static class Result
{
    public static IResult<T, Exception> Ok<T>(T value) => new Ok<T>(value);
    public static IResult<T, Exception> Error<T>(Exception error) => new Error<T, Exception>(error);
    public static IResult<T, TErr> Error<T, TErr>(TErr error) => new Error<T, TErr>(error);
}

public readonly struct Ok<T> : IResult<T, Exception>
{
    private readonly T _value;

    public Ok(T value)
    {
        _value = value;
    }

    public T Value => _value;
    public bool HasValue => true;

    public T ValueOrDefault(T otherwise) => _value;
    public T ValueOrDefault(Func<T> otherwise) => _value;
    public T? ValueOrDefault() => _value;
    public Exception? GetError() => default;
}

public readonly struct Error<T, TErr> : IResult<T, TErr>
{
    private readonly TErr _error;

    public Error(TErr error)
    {
        _error = error;
    }
    
    public bool HasValue => false;
    public TErr Exception => _error; // maybe this should be called something else

    public T ValueOrDefault(T otherwise) => otherwise;
    public T ValueOrDefault(Func<T> otherwise) => otherwise();
    public T? ValueOrDefault() => default;
    public TErr? GetError() => _error;
}