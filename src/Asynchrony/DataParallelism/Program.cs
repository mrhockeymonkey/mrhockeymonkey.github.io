// See https://aka.ms/new-console-template for more information

using System.Diagnostics.Metrics;
using DataParallelism;
using Shared;

Console.WriteLine("Hello, World!");

// sequential sum
long r1 = default;
var t1 = ExecutionTime.Measure(() =>
{
    r1 = PrimeNumbers.PrimeSumSequential();
});
Console.WriteLine($"Sum primes result {r1} took {t1.TotalSeconds}");
//Sum primes result 3203324994356 took 5.0659742

// adding to global total not thread safe implementation
long r2 = default;
var t2 = ExecutionTime.Measure(() =>
{
    r2 = PrimeNumbers.PrimeSumParallelThreadUnsafe();
    // 3149616159166
    // 3148199994203
    // 3149291599408 non deterministic
});
Console.WriteLine($"Sum primes result {r2} took {t2.TotalSeconds}");

// thread safe using Interlocked
long r3 = default;
var t3 = ExecutionTime.Measure(() =>
{
    r3 = PrimeNumbers.PrimeSumParallelThreadSafe();
});
Console.WriteLine($"Sum primes result {r3} took {t3.TotalSeconds}");


// thread safe using thread local state + Interlocked
long r4 = default;
var t4 = ExecutionTime.Measure(() =>
{
    r4 = PrimeNumbers.PrimeSumParallelThreadLocal();
});
Console.WriteLine($"Sum primes result {r4} took {t4.TotalSeconds}");
// Sum primes result 3203324994356 took 1.0289125
