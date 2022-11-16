// See https://aka.ms/new-console-template for more information

using Progress;

Console.WriteLine("Hello, World!");

var total = 500;
await using var periodicProgress = new PeriodicPercentProgress<int>(500);
periodicProgress.ReportTotal(total);
periodicProgress.ProgressChanged += percentComplete => Console.WriteLine($"Percent is {percentComplete:P1}"); 

var toProcess  = Enumerable.Range(0, total);

toProcess.AsParallel().WithDegreeOfParallelism(3).ForAll(i =>
{
    Thread.Sleep(100);
    periodicProgress.Report(i);
});

