namespace CS.Monads.TaskMonads;

public static class TaskExamples
{
    public static async Task RunAsync()
    {
        var initialValue = 10;

        // calling several async methods in sequence 
        var added = await AddFiveAsync(initialValue);
        var longed = await GetLongAsync(added);
        var result1 = await GetStringAsync(longed);

        Console.WriteLine($"Result 1: {result1} should be 15");

        // calling the same but in a more declarative functional way using Bind
        var result2 = await initialValue.ToTask()
            .Bind(AddFiveAsync)
            .Map(AddTwo)
            .Bind(GetLongAsync)
            .Tap(l => Task.Run(() => Console.WriteLine($"Side effect: l is {l}")))
            .Bind(GetStringAsync);

        Console.WriteLine($"Result 2: {result2} should be 17");


// Exactly the same but with LINQ style, here the "Many" is Tasks instead of IEnumerables
        var stringed3 = await AddFiveAsync(initialValue)
            .SelectMany(GetLongAsync)
            .SelectMany(GetStringAsync);

        Console.WriteLine($"Result 3: {stringed3}");


        var add15 = await AddFiveAsync(initialValue)
            .Select(i => i + 5)
            .Select(i => i + 5);


        Console.WriteLine($"{initialValue} + 15 should be {add15}");
    



    }
    
    static async Task<int> AddFiveAsync(int i)
    {
        await Task.Delay(500);
        return i + 5;
    }

    static int AddTwo(int i) => i + 2;

    static async Task<long> GetLongAsync(int i)
    {
        await Task.Delay(500);
        return (long)i;
    }

    static async Task<string> GetStringAsync(long l)
    {
        await Task.Delay(500);
        return l.ToString();
    }
}