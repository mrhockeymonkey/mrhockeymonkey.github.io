using System.Text;

namespace CS.Result.Samples;

public class PatternMatchingSamples
{
    static string HandleResult(IResult<string, Exception> result) => result switch
    {
        Ok<string> y when y.Value.StartsWith("hola") => $"{y.Value} (spanish)",
        Ok<string> x => x.Value,
        Error<string,  ArgumentNullException> ane => $"failed because of an argument null exception: {ane.Exception.Message}",
        Error<string,  ArgumentException> ae => $"failed because of an argument exception: {ae.Exception.Message}",
        Error<string, Exception> ex => $"failed because of an exception: {ex.Exception.Message}",
        _ => throw new ArgumentOutOfRangeException()
    };
    
    public static void Run()
    {
        Console.WriteLine($"----- {nameof(PatternMatchingSamples)} -----");
        // using pattern matching
        Console.WriteLine(HandleResult(Result.Ok<string>("hello world")));
        Console.WriteLine(HandleResult(Result.Ok<string>("hola senor")));
        Console.WriteLine(HandleResult(Result.Error<string, ArgumentNullException>(new ArgumentNullException("foo"))));
        Console.WriteLine(HandleResult(Result.Error<string, ArgumentException>(new ArgumentException("bar"))));
        Console.WriteLine(HandleResult(Result.Error<string, Exception>(new Exception("baz"))));
        Console.WriteLine(HandleResult(Result.Error<string>(new Exception("baz again"))));
        
        // shortcut for when you probably only care about logging the message as a catch all
        // can skip defining exception type which will default to Exception
        Console.WriteLine(HandleResult(Result.Error<string>(new InvalidOperationException())));
    }
}