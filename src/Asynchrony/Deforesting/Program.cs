// See https://aka.ms/new-console-template for more information

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

BenchmarkRunner.Run<Deforesting>();

// |   Method |     Mean |   Error |  StdDev | Ratio | Allocated | Alloc Ratio |
// |--------- |---------:|--------:|--------:|------:|----------:|------------:|
// |   Forest | 950.3 us | 2.92 us | 2.73 us |  1.00 |     393 B |        1.00 |
// | Deforest | 528.9 us | 2.30 us | 2.04 us |  0.56 |      32 B |        0.08 |


[MemoryDiagnoser]
public class Deforesting
{
    private int[] data = new int[100000];
    
    public Deforesting()
    {
        for (int i = 0; i < data.Length; i++)
        {
            data[i] = i;
        }
    }

    [Benchmark(Baseline = true)]
    public long Forest() => data
        .Where(i => i > 10)
        .Where(i => i < 99999)
        .Where(i => i % 2 == 0)
        .Select(i => i + 1)
        .Sum(i => (long)i);

    [Benchmark]
    public long Deforest() => data.Aggregate(0L, (accumulator, i) => i > 10 && i < 9999 && i % 2 == 0
        ? accumulator
        : accumulator + (i + 1));
}