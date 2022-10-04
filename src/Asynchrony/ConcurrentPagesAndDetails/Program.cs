// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using ConcurrentPagesAndDetails;

const int itemCount = 500;
const int maxConcurrentDetails = 5;
const int fakeApiDelayMs = 100;
const int pageSize = 50;

FakeApi api = new(itemCount, fakeApiDelayMs, pageSize);
ConcurrentPagedProcessor pagedProcessor = new(api, maxConcurrentDetails);
ConcurrentAsyncEnumerableProcessor asyncEnumerableProcessor = new(api, maxConcurrentDetails);

var pagedTime = await MeasureAsync(() => pagedProcessor.ProcessAsync());
var asyncEnumerableTime = await MeasureAsync(() => asyncEnumerableProcessor.ProcessAsync());

Console.WriteLine($"{nameof(ConcurrentPagedProcessor)} took {pagedTime.TotalSeconds}");
Console.WriteLine($"{nameof(ConcurrentAsyncEnumerableProcessor)} took {asyncEnumerableTime.TotalSeconds}");

// ConcurrentPagedProcessor finished storing 500 detail objects
// ConcurrentAsyncEnumerableProcessor finished storing 500 detail objects
// ConcurrentPagedProcessor took 11.2294123
// ConcurrentAsyncEnumerableProcessor took 11.1068751

// Since the same amount of work is done in either case it makes sense the performance is the same


static async Task<TimeSpan> MeasureAsync(Func<Task> action)
{
    Stopwatch sw = new();
    sw.Start();
    await action();
    sw.Stop();
    return sw.Elapsed;
}




