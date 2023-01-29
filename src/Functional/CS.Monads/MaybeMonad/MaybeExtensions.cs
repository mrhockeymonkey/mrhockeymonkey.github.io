namespace CS.Monads.MaybeMonad;

public static class MaybeExtensions
{
    public static Maybe<T> ToMaybe<T>(this T value)  => new Some<T>(value); // TODO

    public static Maybe<TResult> Bind<T, TResult>(this Maybe<T> maybe, Func<T, Maybe<TResult>> func) =>
        throw new NotImplementedException(); // TODO

    public static Maybe<TResult> Map<T, TResult>(this Maybe<T> maybe, Func<T, TResult> func)
    {
        switch (maybe)
        {
            case Some<T> some when !EqualityComparer<T>.Default.Equals(some.Value, default):
                try
                {
                    return func(some.Value).ToMaybe();
                }
                catch (Exception)
                {
                    return new None<TResult>();
                }
            default:
                return new None<TResult>();
        }
    }
}