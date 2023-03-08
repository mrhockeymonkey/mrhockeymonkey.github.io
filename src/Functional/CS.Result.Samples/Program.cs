// See https://aka.ms/new-console-template for more information

using CS.Result;
using CS.Result.Samples;

PatternMatchingSamples.Run();

await MethodReturnExamples.RunAsync();

ExtensionsExamples.Run();
        
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

