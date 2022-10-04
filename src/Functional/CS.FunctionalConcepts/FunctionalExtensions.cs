namespace CS;

public static class FunctionalExtensions
{
    public static Func<TA, Func<TB, TR>> Curry<TA, TB, TR>(this Func<TA, TB, TR> func) =>
        a => b => func(a, b);

    public static Func<TA, TB, TR> UnCurry<TA, TB, TR>(this Func<TA, Func<TB, TR>> func) =>
        (a, b) => func(a)(b);

    public static Func<TB, TR> Partial<TA, TB, TR>(this Func<TA, TB, TR> func, TA argA) =>
        argB => func(argA, argB);
}