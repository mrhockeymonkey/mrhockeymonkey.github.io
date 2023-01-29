using System.Diagnostics.CodeAnalysis;

namespace CS.Monads.MaybeMonad;

public abstract class Maybe<T>
{
    public abstract T Value { get; }
    public static implicit operator T(Maybe<T> instance) => instance.Value;
}
public sealed class Some<T> : Maybe<T>
{
    public override T Value { get; }

    public Some(T value)
    {
        Value = value ?? throw new ArgumentNullException(nameof(value));
    }
}

public sealed class None<T> : Maybe<T>
{
    public override T Value => default(T); //TODO
}