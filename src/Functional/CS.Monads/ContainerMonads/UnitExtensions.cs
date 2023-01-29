namespace CS.Monads.ContainerMonads;

public static class UnitExtensions
{
    // Return, a.k.a. unit: a -> E<a>
    // Lifts a single value into the elevated world
    // https://swlaschin.gitbooks.io/fsharpforfunandprofit/content/posts/elevated-world.html#return
    public static Unit<T> ToUnit<T>(this T instance) => new Unit<T>(instance);

    // Bind, a.k.a. flatmap, andThen, collect, selectMany: E<a> -> (a->E<b>) -> E<b>
    // Allows you to compose world-crossing ("monadic") functions
    // https://swlaschin.gitbooks.io/fsharpforfunandprofit/content/posts/elevated-world-2.html#bind
    public static Unit<TResult> Bind<T, TResult>(this Unit<T> unit, Func<T, Unit<TResult>> func) => func(unit.Value);

    // Map, a.k.a. flatmap, select: E<a> -> (a -> b) -> E<b>
    // Lifts a function into the elevated world
    // https://swlaschin.gitbooks.io/fsharpforfunandprofit/content/posts/elevated-world.html#map
    public static Unit<TResult> Map<T, TResult>(this Unit<T> unit, Func<T, TResult> func) => func(unit.Value).ToUnit();
}