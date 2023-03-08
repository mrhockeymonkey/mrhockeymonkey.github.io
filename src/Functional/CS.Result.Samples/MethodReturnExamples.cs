namespace CS.Result.Samples;

public class MethodReturnExamples
{
    public static async Task RunAsync()
    {
        Console.WriteLine($"----- {nameof(MethodReturnExamples)} -----");
        
        var result = GetStringFromBadApi();
        if (result is Error<string, Exception> error)
        {
            Console.WriteLine($"Sadly the error was {error.Exception.GetType()}");
        }

        var asyncResult = await GetStringFromBadApiAsync();
        if (result is Error<string, Exception> error2)
        {
            Console.WriteLine($"Sadly the ASYNC error was {error2.Exception.GetType()}");
        }
    }

    static IResult<string, Exception> GetStringFromBadApi()
    {
        try
        {
            var str = BadMethod();
            return Result.Ok(str);
        }
        catch (Exception ex)
        {
            return Result.Error<string>(ex);
        }
    }
    
    static async Task<IResult<string, Exception>> GetStringFromBadApiAsync()
    {
        try
        {
            await Task.Delay(1000);
            var str = BadMethod();
            return Result.Ok(str);
        }
        catch (Exception ex)
        {
            return Result.Error<string>(ex);
        }
    }

    static string BadMethod() => throw new HttpRequestException();
}