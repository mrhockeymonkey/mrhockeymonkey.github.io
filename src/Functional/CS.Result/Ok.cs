namespace CS.Result;

// TODO provide also Ok<T, TError> allowing any type for error
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