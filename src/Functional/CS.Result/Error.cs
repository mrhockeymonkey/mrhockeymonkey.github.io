namespace CS.Result;

// TODO provide also Error<T, TError> allowing any type for error
public readonly struct Error<T, TError> : IResult<T, TError> where TError : Exception
{
    private readonly TError _error;

    public Error(TError error)
    {
        _error = error;
    }
    
    public bool HasValue => false;
    public TError Exception => _error; // maybe this should be called something else

    public T ValueOrDefault(T otherwise) => otherwise;
    public T ValueOrDefault(Func<T> otherwise) => otherwise();
    public T? ValueOrDefault() => default;
    public TError? GetError() => _error;
}