// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using CS.Memoize;

Console.WriteLine("Hello, World!");

// Memoization is a specific form of caching results of pure functions
// pure functions return the same value when given the same input parameters (referentially transparent)
List<string> input = new(){"red", "blue", "red", "green", "green", "red", "blue", "blue", "red", "blue"};

Func<string, int> parseColor = col =>
{
    Thread.Sleep(500); // some heavy work
    Console.WriteLine($"Parsed {col}");
    return col switch
    {
        "red" => 1, "blue" => 2, "green" => 3, _ => 0
    };
};

var t1 = Measure(() => input.Select(parseColor).ToList());
Console.WriteLine($"parseColor time: {t1}");


Func<string, int> fastParseColor = parseColor.Memoize();
var t2 = Measure(() => input.Select(fastParseColor).ToList());
Console.WriteLine($"fastParseColor time: {t2}");

Func<string, int> threadSafeParseColor = parseColor.MemoizeThreadSafe();
var t3 = Measure(() => input.AsParallel().Select(threadSafeParseColor).ToList());
Console.WriteLine($"threadSafeParseColor time: {t3}");

Func<string, int> threadSafeLazyParseColor = parseColor.MemoizeThreadSafeLazy();
var t4 = Measure(() => input.AsParallel().Select(threadSafeLazyParseColor).ToList());
Console.WriteLine($"threadSafeLazyParseColor time: {t4}");

static TimeSpan Measure(Action action)
{
    var sw = new Stopwatch();
    sw.Start();
    action();
    sw.Stop();
    return sw.Elapsed;
}