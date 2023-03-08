namespace CS.Result;

public interface IResult<T, out TError>
{
    bool HasValue { get; }
    T ValueOrDefault(T otherwise);
    T ValueOrDefault(Func<T> otherwise);
    T? ValueOrDefault();
    TError? GetError();
}