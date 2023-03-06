using System.Diagnostics.Contracts;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using ArgumentException = System.ArgumentException;

namespace Options;

public class ResultExamples
{
    private static string HandleResult(IResult<string, Exception> result) => result switch
    {
        Ok<string> y when y.Value.StartsWith("hola") => $"{y.Value} (spanish)",
        Ok<string> x => x.Value,
        Error<string,  ArgumentNullException> ane => $"failed because of an argument null exception: {ane.Exception.Message}",
        Error<string,  ArgumentException> ae => $"failed because of an argument exception: {ae.Exception.Message}",
        Error<string, Exception> ex => $"failed because of an exception: {ex.Exception.Message}",
        _ => throw new ArgumentOutOfRangeException()
    };
    
    public static async Task Run()
    {
        // using pattern matching
        Console.WriteLine(HandleResult(Result.Ok<string>("hello world")));
        Console.WriteLine(HandleResult(Result.Ok<string>("hola senor")));
        Console.WriteLine(HandleResult(Result.Error<string, ArgumentNullException>(new ArgumentNullException("foo"))));
        Console.WriteLine(HandleResult(Result.Error<string, ArgumentException>(new ArgumentException("bar"))));
        Console.WriteLine(HandleResult(Result.Error<string, Exception>(new Exception("baz"))));
        
        // shortcut for when you probably only care about logging the message as a catch all
        // can skip defining exception type which will default to Exception
        Console.WriteLine(HandleResult(Result.Error<string>(new InvalidOperationException())));
        
        // standard if statement
        var var1 = Result.Ok("hello");
        if (var1 is Ok<string> ok)
        {
            Console.WriteLine($"If got ok: {ok.Value}");
        }
        
        // if statement catching exceptions of derived type
        IResult<object, InvalidOperationException> var2 = Result.Error<object, InvalidOperationException>(new InvalidOperationException());
        if (var2 is IResult<object, Exception> ex)
        {
            Console.WriteLine($"If got some derived exception");
        }


        // use reduce when you dont have a concrete type
        var var3 = Result.Ok(1234);
        Console.WriteLine($"If statement has value of {var3.ValueOrDefault(999)}");
        Console.WriteLine($"If statement has value of {var3.ValueOrDefault(() => 7777)}");
    }
}