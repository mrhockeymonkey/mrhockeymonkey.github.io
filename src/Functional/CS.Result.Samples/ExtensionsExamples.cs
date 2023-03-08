namespace CS.Result.Samples;

public class ExtensionsExamples
{
    public static void Run()
    {
        Console.WriteLine($"----- {nameof(ExtensionsExamples)} -----");

        var alphabet = "a".ToResult()
            .Map(s => string.Concat(s, "b"))
            .Map(s => string.Concat(s, "c"))
            .Map(s => string.Concat(s, "d"));
        
        Console.WriteLine($"alphabet is: {alphabet.ValueOrDefault()}");

        var journey = 1.ToResult()
            .Map(i => (double)i)
            .Bind(d => Result.Ok((float)d)) 
            .Map(d => (decimal)d);
        
        Console.WriteLine($"journey ended up as {journey.ValueOrDefault().GetType()}");

        var shortcut = "hello".ToResult()
            .Bind(s => Result.Error<string>(new Exception("broken")))
            .Map(s => $"{s} world");

        if (shortcut is Error<string, Exception> err) Console.WriteLine(err.Exception.Message);
        else Console.WriteLine(shortcut is Ok<string> x); // TOOD not sure what to do here

    }
}