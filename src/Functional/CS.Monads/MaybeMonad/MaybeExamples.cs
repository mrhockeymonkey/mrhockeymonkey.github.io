namespace CS.Monads.MaybeMonad;

public static class MaybeExamples
{
    public static void Run()
    {
        var initialValue = 1;

        var result = initialValue.ToMaybe()
            .Map(i => i + 1)
            .Map(i => i / 0) 
            .Map(i => i + 1); // short circuited
        
        Console.WriteLine($"{result.Value} should be 0");
    }
}